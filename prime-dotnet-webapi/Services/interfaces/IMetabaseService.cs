using System.Threading.Tasks;
using Prime.Models.Api;
namespace Prime.Services
{
    public interface IMetabaseService
    {
        string BuildMetabaseEmbeddedUrl();
    }
}
