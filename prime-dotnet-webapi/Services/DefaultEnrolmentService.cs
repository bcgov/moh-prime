using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolmentService : IEnrolmentService
    {
        private readonly ApiDbContext _context;

        public DefaultEnrolmentService(
            ApiDbContext context)
        {
            _context = context;
        }

        public bool EnrolmentExists(int enrolmentId)
        {
            return _context.Enrolments.Any(e => e.Id == enrolmentId);
        }

        public async Task<Enrolment> GetEnrolmentAsync(int enrolmentId)
        {
            var entity = await _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .SingleOrDefaultAsync(e => e.Id == enrolmentId)
                ;

            return entity;
        }

        public async Task<Enrolment> GetEnrolmentForUserIdAsync(string userId)
        {
            var entity = await _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .SingleOrDefaultAsync(e => e.Enrollee.UserId == userId)
                ;

            return entity;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsAsync()
        {
            IQueryable<Enrolment> query = _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                ;

            var items = await query.ToArrayAsync();

            return items;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(
            string userId)
        {
            IQueryable<Enrolment> query = _context.Enrolments
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.PhysicalAddress)
                .Include(e => e.Enrollee)
                .ThenInclude(e => e.MailingAddress)
                .Include(e => e.Certifications)
                .Include(e => e.Jobs)
                .Include(e => e.Organizations)
                .Where(e => e.Enrollee.UserId == userId)
                ;

            var items = await query.ToArrayAsync();

            return items;
        }

        public async Task<int?> CreateEnrolmentAsync(Enrolment enrolment)
        {
            enrolment.AppliedDate = DateTime.Now;
            _context.Enrolments.Add(enrolment);

            var created = await _context.SaveChangesAsync();
            if (created < 1) throw new InvalidOperationException("Could not create enrolment.");

            return enrolment.Id;
        }

        public async Task<int> UpdateEnrolmentAsync(Enrolment enrolment)
        {
            //get the enrollee from the enrolment
            Enrollee _enrollee = enrolment.Enrollee;
            var _enrolleeDb = _context.Enrollees.Include(e => e.PhysicalAddress).Include(e => e.MailingAddress).AsNoTracking().Where(e => e.Id == _enrollee.Id).FirstOrDefault();
            var _enrolmentDb = _context.Enrolments.Include(e => e.Certifications).Include(e => e.Jobs).Include(e => e.Organizations).AsNoTracking().Where(e => e.Id == enrolment.Id).FirstOrDefault();

            // remove existing addresses
            if (_enrolleeDb.PhysicalAddress != null)
            {
                _enrolleeDb.PhysicalAddress.Enrollee = null;
                _context.Addresses.Remove(_enrolleeDb.PhysicalAddress);
            }
            if (_enrolleeDb.MailingAddress != null)
            {
                _enrolleeDb.MailingAddress.Enrollee = null;
                _context.Addresses.Remove(_enrolleeDb.MailingAddress);
            }

            // create the new addresses, if they exist
            PhysicalAddress _physicalAddress = _enrollee.PhysicalAddress;
            if (_physicalAddress != null)
            {
                _physicalAddress.EnrolleeId = (int)_enrollee.Id;
                _context.Entry(_physicalAddress).State = EntityState.Added;
            }
            MailingAddress _mailingAddress = _enrollee.MailingAddress;
            if (_mailingAddress != null)
            {
                _mailingAddress.EnrolleeId = (int)_enrollee.Id;
                _context.Entry(_mailingAddress).State = EntityState.Added;
            }

            // remove existing certifications
            foreach (var certification in _enrolmentDb.Certifications)
            {
                certification.Enrolment = null;
                _context.Certifications.Remove(certification);
            }
            // create new certifications
            if (enrolment.Certifications != null)
            {
                foreach (var certification in enrolment.Certifications)
                {
                    certification.EnrolmentId = (int)enrolment.Id;
                    _context.Entry(certification).State = EntityState.Added;
                }
            }

            // remove existing jobs
            foreach (var job in _enrolmentDb.Jobs)
            {
                job.Enrolment = null;
                _context.Jobs.Remove(job);
            }
            // create new jobs
            if (enrolment.Jobs != null)
            {
                foreach (var job in enrolment.Jobs)
                {
                    job.EnrolmentId = (int)enrolment.Id;
                    _context.Entry(job).State = EntityState.Added;
                }
            }

            // remove existing organizations
            foreach (var organization in _enrolmentDb.Organizations)
            {
                organization.Enrolment = null;
                _context.Organizations.Remove(organization);
            }
            if (enrolment.Organizations != null)
            {
                // create new organizations
                foreach (var organization in enrolment.Organizations)
                {
                    organization.EnrolmentId = (int)enrolment.Id;
                    _context.Entry(organization).State = EntityState.Added;
                }
            }

            //update the enrolment to include the enrolleeId
            enrolment.EnrolleeId = (int)_enrollee.Id;
            _context.Entry(enrolment).State = EntityState.Modified;

            _context.Entry(_enrollee).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public async Task DeleteEnrolmentAsync(int enrolmentId)
        {
            var enrolment = await _context.Enrolments
                .SingleOrDefaultAsync(e => e.Id == enrolmentId);
            if (enrolment == null) return;

            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();
        }
    }
}
