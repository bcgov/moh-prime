namespace Prime.Services
{
    public interface IPdfService
    {
        byte[] Generate(string htmlContent);
    }
}
