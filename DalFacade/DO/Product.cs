

namespace DO;

public struct Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
Product Id={Id}: {Name}, 
category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";


}

