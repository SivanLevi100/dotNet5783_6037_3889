using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public class Factory
{
    public static IBl? Get()
    {
       return new BlImplementation.Bl();
    }


}
