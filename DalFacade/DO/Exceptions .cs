using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO;

[Serializable]
public class DoesNotExistException : Exception
{
    public DoesNotExistException()
    {
    }

    public DoesNotExistException(string? message) : base(message)
    {
    }

    public DoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
public class DataCorruptionException : Exception
{
    public DataCorruptionException()
    {
    }

    public DataCorruptionException(string? message) : base(message)
    {
    }

    public DataCorruptionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DataCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
[Serializable]
public class NotFoundExceptions: Exception
{
    public NotFoundExceptions()
    {

    }
    public NotFoundExceptions(string? message):base(message)
    {

    }
    public NotFoundExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
    public NotFoundExceptions(SerializationInfo info, StreamingContext context): base(info,context)
    {

    }
}
public class DuplicateIdExceptions: Exception
{
    public DuplicateIdExceptions()
    {

    }
    public DuplicateIdExceptions(string? message) : base(message)
    {

    }
    public DuplicateIdExceptions(string? message, Exception? innerException) : base(message, innerException)
    {

    }
    public DuplicateIdExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}