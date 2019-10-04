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

            if (entity == null) return null;

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

            if (entity == null) return null;

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
            //enrolment.EnrolleeId = enrolment.Enrollee.Id;
            _context.Entry(enrolment).State = EntityState.Modified;
            foreach (var certification in enrolment.Certifications)
            {
                if (certification.Id == null)
                {
                    _context.Entry(certification).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(certification).State = EntityState.Modified;
                }
            }

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
