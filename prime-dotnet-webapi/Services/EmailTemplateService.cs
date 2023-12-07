using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public class EmailTemplateService : BaseService, IEmailTemplateService
    {
        private readonly IMapper _mapper;

        public EmailTemplateService(
            ApiDbContext context,
            ILogger<EmailTemplateService> logger,
            IMapper mapper)
            : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<bool> EmailTemplateExistsAsync(int id)
        {
            return await _context.EmailTemplates
                .AsNoTracking()
                .AnyAsync(e => e.Id == id);
        }

        public async Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateType type)
        {
            return await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.EmailType == type);
        }

        public async Task<IEnumerable<EmailTemplateListViewModel>> GetEmailTemplatesAsync()
        {
            return await _context.EmailTemplates
                .ProjectTo<EmailTemplateListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<EmailTemplateViewModel> GetEmailTemplateAsync(int id)
        {
            return await _context.EmailTemplates
                .Where(t => t.Id == id)
                .ProjectTo<EmailTemplateViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<EmailTemplateViewModel> UpdateEmailTemplateAsync(int id, string template)
        {
            var emailTemplate = await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.Id == id);

            emailTemplate.Template = template;
            emailTemplate.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }

        public async Task<EmailTemplateViewModel> UpdateEmailSubjectAsync(int id, string subject)
        {

            var emailTemplate = await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.Id == id);

            emailTemplate.Subject = subject;
            emailTemplate.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }


        public async Task<EmailTemplateViewModel> UpdateEmailTitleAsync(int id, string title)
        {

            var emailTemplate = await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.Id == id);

            emailTemplate.TemplateName = title;
            emailTemplate.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }

        public async Task<EmailTemplateViewModel> UpdateEmailDescriptionAsync(int id, string description)
        {

            var emailTemplate = await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.Id == id);

            emailTemplate.Description = description;
            emailTemplate.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }

        public async Task<EmailTemplateViewModel> UpdateEmailRecipientAsync(int id, string recipient)
        {

            var emailTemplate = await _context.EmailTemplates
                .SingleOrDefaultAsync(t => t.Id == id);

            emailTemplate.Recipient = recipient;
            emailTemplate.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }
    }
}
