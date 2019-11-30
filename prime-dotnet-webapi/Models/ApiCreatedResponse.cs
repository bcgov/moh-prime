using Microsoft.AspNetCore.Http;

namespace Prime.Models
{
    public class ApiCreatedResponse<T> : ApiResponse where T : class
    {
        public T Result { get; }

        public ApiCreatedResponse(T result) : base(StatusCodes.Status201Created)
        {
            Result = result;
        }
    }
}
