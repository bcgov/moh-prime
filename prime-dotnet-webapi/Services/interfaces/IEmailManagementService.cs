using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.HttpClients.Mail;

namespace Prime.Services
{
    public interface IEmailManagementService
    {
        Task<string> GetPharmaNetProvisionerEmailAsync(string provisionerName);
        Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync();
        Task CreateChesEmailLog(Email email, Guid? msgId);
        Task CreateSmtpEmailLog(Email email);
        Task<bool> UpdateEmailLogStatuses();
    }
}
