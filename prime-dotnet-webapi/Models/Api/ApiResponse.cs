using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prime.Models.Api
{
    public static class ApiResponse
    {
        public static ApiResultResponse<T> Result<T>(T result)
        {
            return new ApiResultResponse<T>(result);
        }

        public static ApiMessageResponse Message(string message)
        {
            return new ApiMessageResponse(message);
        }
    }

    public class ApiResultResponse<T>
    {
        public T Result { get; }

        public ApiResultResponse(T result)
        {
            Result = result;
        }
    }

    public class ApiMessageResponse
    {
        public string Message { get; }

        public ApiMessageResponse(string message)
        {
            Message = message;
        }
    }
}
