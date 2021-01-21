using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DelegateDecompiler.EntityFrameworkCore;

using Prime.Models;
using Prime.HttpClients;
using Prime.Services.Razor;
using Prime.HttpClients.Mail;
using Prime.HttpClients.Mail.ChesApiDefinitions;
using Prime.Engines;

namespace Prime.Services
{
    public class EmailManagementService : BaseService, IEmailManagementService
    {
        private static class SendType
        {
            public const string Ches = "CHES";
            public const string Smtp = "SMTP";
        }

        private readonly IChesClient _chesClient;

        public EmailManagementService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IChesClient chesClient)
            : base(context, httpContext)
        {
            _chesClient = chesClient;
        }

        public async Task<string> GetPharmaNetProvisionerEmailAsync(string provisionerName)
        {
            var vendor = await _context.Vendors
                .SingleOrDefaultAsync(v => v.Name == provisionerName);

            return vendor?.Email;
        }

        public async Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync()
        {
            return await _context.Vendors
                .Select(v => v.Name)
                .ToListAsync();
        }

        public async Task<bool> UpdateEmailLogStatuses()
        {
            var emailLogs = await _context.EmailLogs
                .Where(e => e.SendType == SendType.Ches
                    && e.MsgId != null
                    && e.LatestStatus != ChesStatus.Completed)
                .ToListAsync();

            foreach (var email in emailLogs)
            {
                var status = await _chesClient.GetStatusAsync(email.MsgId.Value);
                if (status != null && email.LatestStatus != status)
                {
                    email.LatestStatus = status;
                }
            }

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task CreateChesEmailLog(Email email, Guid? msgId)
        {
            _context.EmailLogs.Add(new EmailLog
            {
                SentTo = string.Join(",", email.To),
                Cc = string.Join(",", email.Cc),
                Subject = email.Subject,
                Body = email.Body,
                SendType = SendType.Ches,
                MsgId = msgId,
                DateSent = DateTimeOffset.Now
            });

            await _context.SaveChangesAsync();
        }

        public async Task CreateSmtpEmailLog(Email email)
        {
            _context.EmailLogs.Add(new EmailLog
            {
                SentTo = string.Join(",", email.To),
                Cc = string.Join(",", email.Cc),
                Subject = email.Subject,
                Body = email.Body,
                SendType = SendType.Smtp,
                DateSent = DateTimeOffset.Now
            });

            await _context.SaveChangesAsync();
        }
    }
}
