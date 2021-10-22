namespace Pip.Services
{
    public interface IFirstService
    {
        Task<bool> GetSomethingAsync();

        Task<IEnumerable<string>> GetLookupModelNamesAsync();

    }
}
