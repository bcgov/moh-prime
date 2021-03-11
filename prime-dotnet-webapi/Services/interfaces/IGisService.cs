using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IGisService
    {
        Task<bool> LdapLogin(string username, string password);
    }
}
