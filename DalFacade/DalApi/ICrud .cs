using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    T Add(T entity);
    T Get(T entity);
    T Delete(T entity);
    T Update(T entity);
    IEnumerable<T> GetArray();

}
