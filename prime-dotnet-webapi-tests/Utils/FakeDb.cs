using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FakeItEasy;
using Prime;
using Prime.Models;

namespace PrimeTests.Utils
{
    public static class AFake
    {
        public static ApiDbContext Db()
        {
            var fakeContext = A.Fake<ApiDbContext>();
            A.CallTo(() => fakeContext.SaveChangesAsync(default(CancellationToken))).Returns(1);
            return fakeContext;
        }

        public static ApiDbContext WithEnrollees(this ApiDbContext fakeDb, IEnumerable<Enrollee> enrollees)
        {
            A.CallTo(() => fakeDb.Enrollees).Returns(DbSet(enrollees));

            return fakeDb;
        }

        public static DbSet<TEntity> DbSet<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            var fakeDbSet = A.Fake<DbSet<TEntity>>(options => options.Implements(typeof(IQueryable<TEntity>)));
            A.CallTo(() => ((IQueryable<TEntity>)fakeDbSet).Provider).Returns(data.AsQueryable().Provider);

            return fakeDbSet;
        }
    }
}
