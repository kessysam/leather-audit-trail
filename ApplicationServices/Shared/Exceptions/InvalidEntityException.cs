using System;

namespace ApplicationServices.Shared.Exceptions
{
    public class InvalidEntityException:Exception
    {
        public string Code { get; set; }
        public InvalidEntityException()
        { }

        public InvalidEntityException(string message, string code="InvalidEntity")
            : base(message)
        {
            Code = code;
        }

        public InvalidEntityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}