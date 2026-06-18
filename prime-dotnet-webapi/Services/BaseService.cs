using Microsoft.Extensions.Logging;

namespace Prime.Services
{
    static class CollegeCode
    {
        public const int CPSBC = 1;
        public const int BCCNM = 3;
    }

    static class LicenseCode
    {
        public const int PharmacyTechnician = 29;
        public const int NaturopathicFull = 166;
        public const int NaturopathicTemporay = 167;
        public const int NaturopathicStudent = 168;
    }


    public abstract class BaseService
    {
        protected const int InvalidId = -1;

        protected readonly ApiDbContext _context;
        protected readonly ILogger _logger;

        protected BaseService(ApiDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
