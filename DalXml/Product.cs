using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;
using System.Xml.Linq;


//implement IStudent with linq to XML

internal class Product : IProduct
{
    const string p_products = "products"; //Linq to XML

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
       filter is null
       ? XMLTools.LoadListFromXMLElement(p_products).Elements().Select(s => getProduct(s))
       : XMLTools.LoadListFromXMLElement(p_products).Elements().Select(s => getProduct(s)).Where(filter);

    public DO.Product? Get(int id) =>
       (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(p_products)?.Elements()
       .FirstOrDefault(st => st.ToIntNullable("Id") == id)
       ?? throw new NotFoundExceptions("The product id is not exist in List"))!;


    public int Add(DO.Product product)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(p_products);

        if (XMLTools.LoadListFromXMLElement(p_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("Id") == product.Id) is not null)
            throw new DuplicateIdExceptions("No place in List to add");

        productsRootElem.Add(new XElement("Product", createProductElement(product)));
        XMLTools.SaveListToXMLElement(productsRootElem, p_products);

        return product.Id;
    }


    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(p_products);

        (productsRootElem.Elements()
            .FirstOrDefault(st => (int?)st.Element("Id") == id) ?? throw new NotFoundExceptions("The product is not exist in the List"))
            .Remove();

        XMLTools.SaveListToXMLElement(productsRootElem, p_products);
    }

    public void Update(DO.Product doProduct)
    {
        Delete(doProduct.Id);
        Add(doProduct);
    }


    public DO.Product GetF(Func<DO.Product?, bool>? filter)////////לשאול?
    {
        throw new NotFoundExceptions("The product");

       // return (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(p_products)?.Elements()
       //.FirstOrDefault(st => st.ToIntNullable("Id") == id);
    }



    static DO.Product? getProduct(XElement product)
    {
        return product.ToIntNullable("Id") is null ? null : new DO.Product()
        {
            Id = (int)product.Element("Id")!,
            Name = (string?)product.Element("Name"),
            Category = product.ToEnumNullable<DO.Category>("Category"),
            Price = (double)product.Element("Price")!,
            InStock = (int)product.Element("InStock")!
        };

        //(double)product.Element("Price")!,
       // double.Parse(product.Element("Price").Value),
    }
    static IEnumerable<XElement> createProductElement(DO.Product product)
    {
        yield return new XElement("Id", product.Id);

        if (product.Name is not null)
            yield return new XElement("Name", product.Name);
        if (product.Category is not null)
            yield return new XElement("Category", product.Category);
      
        yield return new XElement("Price", product.Price);
        yield return new XElement("InStock", product.InStock);

        //if (product.Price is not null)
        //    yield return new XElement("Price", product.Price);
        //if (product.InStock is not null)
        //    yield return new XElement("InStock", product.InStock);
    }


}
