using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IFeedbackService
    {

        Task<int> CreateFeedbackAsync(Feedback feedback);
    }
}
