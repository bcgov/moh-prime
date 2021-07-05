using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateType type);
        Task<IEnumerable<EmailTemplateListViewModel>> GetEmailTemplatesAsync();
        Task<EmailTemplateViewModel> GetEmailTemplateAsync(int id);
        Task<int> UpdateEmailTemplateAsync(int id, EmailTemplateViewModel updateModel);
    }
}
