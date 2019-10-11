namespace Prime.Models
{
    public class ApiOkResponse<T> : ApiResponse where T : class
    {
        public T Result { get; }

        public ApiOkResponse(T result) : base(200)
        {
            Result = result;
        }
    }
}