namespace Pidp.Models.Lookups
{
    public interface ILookupDataGenerator<T>
    {
        IEnumerable<T> Generate();
    }
}
