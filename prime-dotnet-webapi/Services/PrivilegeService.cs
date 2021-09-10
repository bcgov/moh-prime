using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Prime.Models;

namespace Prime.Services
{
    public class PrivilegeService : BaseService, IPrivilegeService
    {
        public PrivilegeService(
            ApiDbContext context,
            ILogger<PrivilegeService> logger)
            : base(context, logger)
        { }

        public async Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee)
        {
            ICollection<AssignedPrivilege> assignedPrivileges = await GetAssignedPrivilegesForEnrolleeAsync(enrolleeId);

            if (assignedPrivileges != null)
            {
                foreach (var privilege in assignedPrivileges)
                {
                    privilege.Enrollee = null;
                    privilege.Privilege = null;
                    _context.Remove(privilege);
                }
            }

            await _context.SaveChangesAsync();

            var _enrolleeDb = _context.Enrollees
                                .Include(e => e.Certifications)
                                .Where(e => e.Id == enrollee.Id)
                                .SingleOrDefault();

            ICollection<Certification> certifications = _enrolleeDb.Certifications;
            if (certifications != null)
            {
                ICollection<DefaultPrivilege> defaultPrivileges = new List<DefaultPrivilege>();
                foreach (var cert in certifications)
                {
                    ICollection<DefaultPrivilege> result = await GetDefaultPrivilegesForLicenseCodeAsync(cert.LicenseCode);

                    foreach (var p in result)
                    {
                        if (!defaultPrivileges.Any(df => df.PrivilegeId == p.PrivilegeId))
                        {
                            defaultPrivileges.Add(p);
                        }
                    }
                }

                if (defaultPrivileges != null)
                {
                    foreach (var defaultPrivilege in defaultPrivileges)
                    {
                        AssignedPrivilege assignedPrivilege = new AssignedPrivilege
                        {
                            EnrolleeId = enrolleeId,
                            PrivilegeId = defaultPrivilege.PrivilegeId
                        };

                        _context.Entry(assignedPrivilege).State = EntityState.Added;
                    }
                }

                _context.Entry(_enrolleeDb).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Privilege>> GetPrivilegesForEnrolleeAsync(Enrollee enrollee)
        {
            ICollection<Privilege> privileges = new List<Privilege>();
            var results = await GetPrivilegesForEnrolleeQueryAsync(enrollee.Id);

            foreach (var item in results)
            {
                privileges.Add(item);
            }

            return privileges;
        }

        private async Task<ICollection<DefaultPrivilege>> GetDefaultPrivilegesForLicenseCodeAsync(int licenseCode)
        {
            return await _context.DefaultPrivileges
                        .Where(df => df.LicenseCode == licenseCode)
                        .ToListAsync();
        }

        public async Task<ICollection<AssignedPrivilege>> GetAssignedPrivilegesForEnrolleeAsync(int enrolleeId)
        {
            return await _context.AssignedPrivileges
                        .Where(ap => ap.EnrolleeId == enrolleeId)
                        .Include(ap => ap.Privilege)
                            .ThenInclude(pg => pg.PrivilegeGroup)
                                .ThenInclude(pt => pt.PrivilegeType)
                        .ToListAsync();
        }

        private async Task<ICollection<Privilege>> GetPrivilegesForEnrolleeQueryAsync(int enrolleeId)
        {
            return await _context.Privileges
                        .Include(p => p.AssignedPrivileges)
                        .Include(pg => pg.PrivilegeGroup)
                            .ThenInclude(pt => pt.PrivilegeType)
                        .Where(p => p.AssignedPrivileges.Any(ap => ap.EnrolleeId == enrolleeId))
                        .ToListAsync();
        }

    }
}
