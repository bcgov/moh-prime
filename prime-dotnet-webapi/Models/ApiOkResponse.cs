using Microsoft.AspNetCore.Http;

namespace Prime.Models
{
    public class ApiOkResponse<T> : ApiResponse where T : class
    {
        public T Result { get; }

        public ApiOkResponse(T result) : base(StatusCodes.Status200OK)
        {
            Result = result;
        }
    }
}
