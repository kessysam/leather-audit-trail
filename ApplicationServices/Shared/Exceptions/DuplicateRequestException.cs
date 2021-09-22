using System;

namespace ApplicationServices.Shared.Exceptions
{
    public class DuplicateRequestException: Exception
    {
        public string Code { get; set; }
        public DuplicateRequestException()
        { }

        public DuplicateRequestException(string message, string code="DuplicateRequest")
            : base(message)
        {
            Code = code;
        }

        public DuplicateRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}