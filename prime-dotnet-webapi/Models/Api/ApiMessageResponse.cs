namespace Prime.Models.Api
{
    public class ApiMessageResponse
    {
        public string Message { get; }

        public ApiMessageResponse(string message)
        {
            Message = message;
        }
    }
}
