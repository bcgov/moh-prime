namespace Prime.Models.Api
{
    public class ApiResultResponse<T>
    {
        public T Result { get; }

        public ApiResultResponse(T result)
        {
            Result = result;
        }
    }
}
