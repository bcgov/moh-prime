using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime;
using PrimeTests.Utils;

namespace PrimeTests.Services
{
    public class BaseServiceTests<T> : IDisposable where T : class
    {
        protected string _databaseName;
        protected ApiDbContext _dbContext;
        protected T _service;

        public BaseServiceTests()
        {
            _databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ApiDbContext>()
                        .UseInMemoryDatabase(databaseName: _databaseName)
                      .Options;

            var context = new HttpContextAccessor();
            context.HttpContext = new DefaultHttpContext();

            _dbContext = new ApiDbContext(options, context);

            TestUtils.InitializeDbForTests(_dbContext);

            _service = (T)Activator.CreateInstance(typeof(T), new object[] { _dbContext, });
        }

        public void Dispose()
        {
            TestUtils.DetachAllEntities(_dbContext);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}