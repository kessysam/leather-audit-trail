using System;

namespace ApplicationServices.Shared.Exceptions
{
    public class OperationInvalidException: Exception
    {
        public string Code { get; set; }
        public OperationInvalidException()
        { }

        public OperationInvalidException(string message, string code = "InvalidOperation")
            : base(message)
        {
            Code = code;
        }

        public OperationInvalidException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
