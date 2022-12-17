using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T entity);
    T Get(int entity);
    void Delete(int entity);
    void Update(T entity);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);

}
