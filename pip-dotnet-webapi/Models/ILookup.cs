namespace Pidp.Models
{
    public interface ILookup<T>
    {
        T Code { get; set; }

        string Name { get; set; }
    }
}
