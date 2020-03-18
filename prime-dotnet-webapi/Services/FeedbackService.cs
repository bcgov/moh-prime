using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class FeedbackService : BaseService, IFeedbackService
    {
        public FeedbackService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<int> CreateFeedbackAsync(Feedback feedback)
        {
            _context.Feedback.Add(feedback);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create feedback.");
            }

            return feedback.Id;
        }
    }
}
