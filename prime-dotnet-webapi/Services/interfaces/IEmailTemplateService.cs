using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public interface IEmailTemplateService
    {
        Task<bool> EmailTemplateExistsAsync(int id);
        Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateType type);
        Task<IEnumerable<EmailTemplateListViewModel>> GetEmailTemplatesAsync();
        Task<EmailTemplateViewModel> GetEmailTemplateAsync(int id);
        Task<EmailTemplateViewModel> UpdateEmailTemplateAsync(int id, string template);
        Task<EmailTemplateViewModel> UpdateEmailSubjectAsync(int id, string subject);
        Task<EmailTemplateViewModel> UpdateEmailTitleAsync(int id, string title);
        Task<EmailTemplateViewModel> UpdateEmailRecipientAsync(int id, string recipient);
        Task<EmailTemplateViewModel> UpdateEmailDescriptionAsync(int id, string description);
    }
}
