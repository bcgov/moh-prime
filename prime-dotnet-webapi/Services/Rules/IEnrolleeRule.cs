using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services.Rules
{
    public interface IEnrolleeRule
    {
        Task<bool> ProcessRule(Enrollee enrollee);
    }
}
