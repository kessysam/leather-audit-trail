using System;

namespace ApplicationServices.Shared.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string Code { get; set; }
        public EntityNotFoundException()
        { }

        public EntityNotFoundException(string message, string code="ArgumentNull")
            : base(message)
        {
            Code = code;
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
