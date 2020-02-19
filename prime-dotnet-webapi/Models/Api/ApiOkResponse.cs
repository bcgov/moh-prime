using Microsoft.AspNetCore.Http;

namespace Prime.Models.Api
{
    public class ApiOkResponse<T> : ApiResponse where T : class
    {
        public T Result { get; }

        public ApiOkResponse(T result) : this(result, null)
        {
        }

        public ApiOkResponse(T result, string message) : base(StatusCodes.Status200OK, message)
        {
            Result = result;
        }

    }
}
