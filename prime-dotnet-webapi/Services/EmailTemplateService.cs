using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public class EmailTemplateService : BaseService, IEmailTemplateService
    {
        private readonly IMapper _mapper;
        public EmailTemplateService(
            ApiDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<EmailTemplate> GetEmailTemplateByTypeAsync(EmailTemplateType type)
        {
            return await _context.EmailTemplates.Where(t => t.EmailType == type).SingleOrDefaultAsync();
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
            var emailTemplate = await _context.EmailTemplates.Where(t => t.Id == id).SingleOrDefaultAsync();

            emailTemplate.Template = template;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
        }
    }
}
