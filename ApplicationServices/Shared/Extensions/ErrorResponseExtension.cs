using System;
using ApplicationServices.Shared.ErrorModel;
using ApplicationServices.Shared.Exceptions;

namespace ApplicationServices.Shared.Extensions
{
    public static class ErrorResponseExtension
    {
        public static ErrorResponse ChangeToError(this Exception exception)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = "SYSTEM_ERROR",
                Message = "Unexpected error occured please try again or confirm current operation status"
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this BadRequestException badRequestException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = badRequestException.Code,
                Message = badRequestException.Message
            });
            return errorResponse;
        }
        
        public static ErrorResponse ChangeToError(this InvalidEntityException badRequestException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = badRequestException.Code,
                Message = badRequestException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this ArgumentIsNullException argumentIsNullException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = argumentIsNullException.Code,
                Message = argumentIsNullException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this NotFoundException notFoundException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = notFoundException.Code,
                Message = notFoundException.Message
            });
            return errorResponse;
        }
        
        public static ErrorResponse ChangeToError(this EntityNotFoundException entityNotFoundException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = entityNotFoundException.Code,
                Message = entityNotFoundException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this UnAuthorizedException unAuthorizedException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error
            {
                Code = unAuthorizedException.Code,
                Message = unAuthorizedException.Message
            });
            return errorResponse;
        }

        // public static ErrorResponse ChangeToError(this ArgumentIsOutOfRangeException unAuthorizedException)
        // {
        //     var errorResponse = new ErrorResponse();
        //     errorResponse.Errors.Add(new Error
        //     {
        //         Code = unAuthorizedException.Code,
        //         Message = unAuthorizedException.Message
        //     });
        //     return errorResponse;
        // }
    }
}
