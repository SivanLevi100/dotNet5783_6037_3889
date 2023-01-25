using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;

//internal class Exception
//{


//}

/// <summary>
/// Throws exceptions of type nonexistent instance
/// </summary>
public class NotExiestsExceptions : Exception
{
    public NotExiestsExceptions()
    {

    }
    public NotExiestsExceptions(string? message) : base(message)
    {

    }
    public NotExiestsExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
    public NotExiestsExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }



}

/// <summary>
/// Exception class for an invalid data type
/// </summary>
public class IncorrectDataExceptions : Exception
{
    public IncorrectDataExceptions()
    {

    }
    public IncorrectDataExceptions(string? message) : base(message)
    {

    }
    public IncorrectDataExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
    public IncorrectDataExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

}
