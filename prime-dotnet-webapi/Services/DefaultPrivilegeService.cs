using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultPrivilegeService : BaseService, IPrivilegeService
    {
        public DefaultPrivilegeService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee)
        {
            var _enrolleeDb = _context.Enrollees
                                .Include(e => e.Certifications)
                                .Where(e => e.Id == enrollee.Id)
                                .AsNoTracking()
                                .SingleOrDefault();

            ICollection<AssignedPrivilege> assignedPrivileges = this.GetAssignedPrivilegesForEnrollee(enrolleeId);


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

            _enrolleeDb = _context.Enrollees
                                .Include(e => e.Certifications)
                                .Where(e => e.Id == enrollee.Id)
                                .SingleOrDefault();

            ICollection<Certification> certifications = _enrolleeDb.Certifications;
            if (certifications != null)
            {
                ICollection<DefaultPrivilege> defaultPrivileges = new List<DefaultPrivilege>();
                foreach (var cert in certifications)
                {
                    ICollection<DefaultPrivilege> result = this.GetDefaultPrivilegesForLicenseCode(cert.LicenseCode);

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

                await _context.SaveChangesAsync();
            }
        }

        private ICollection<DefaultPrivilege> GetDefaultPrivilegesForLicenseCode(int licenseCode)
        {
            return _context.DefaultPrivileges
                        .Where(df => df.LicenseCode == licenseCode)
                        .ToList();
        }

        public ICollection<AssignedPrivilege> GetAssignedPrivilegesForEnrollee(int? enrolleeId)
        {
            return _context.AssignedPrivileges
                        .Where(ap => ap.EnrolleeId == enrolleeId)
                        .Include(ap => ap.Privilege)
                        .ToList();
        }

    }
}
