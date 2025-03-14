using System;
using System.Linq;
using Xunit;

using Prime.Engines;
using Prime.Models;

namespace PrimeTests.UnitTests
{
    public class EntityMatcherTests
    {
        [Fact]
        public void TestEntityMatcher_EmptyExisting()
        {
            var existing = new Entity[] { };
            var incoming = new[]
            {
                new ViewModel(0),
                new ViewModel(0),
                new ViewModel(1)
            };

            var matches = EntityMatcher
                .MatchUsing((Entity e) => e.Id, (ViewModel v) => v.Id)
                .Match(existing, incoming);

            Assert.Empty(matches.Updated);
            Assert.Empty(matches.Dropped);
            Assert.Equal(incoming, matches.Added);
        }

        [Fact]
        public void TestEntityMatcher_EmptyIncoming()
        {
            var existing = new[]
            {
                new Entity(1),
                new Entity(2)
            };
            var incoming = new ViewModel[] { };

            var matches = EntityMatcher
                .MatchUsing((Entity e) => e.Id, (ViewModel v) => v.Id)
                .Match(existing, incoming);

            Assert.Empty(matches.Updated);
            Assert.Equal(existing, matches.Dropped);
            Assert.Empty(matches.Added);
        }

        [Fact]
        public void TestEntityMatcher_Updates()
        {
            var existing = new[]
            {
                new Entity(1),
                new Entity(2),
                new Entity(3),
                new Entity(4)
            };
            var incoming = new[]
            {
                new ViewModel(0),
                new ViewModel(0),
                new ViewModel(2),
                new ViewModel(3),
                new ViewModel(12)
            };

            var matches = EntityMatcher
                .MatchUsing((Entity e) => e.Id, (ViewModel v) => v.Id)
                .Match(existing, incoming);

            var expectedUpdated = new[]
            {
                (existing.Single(x => x.Id == 2), incoming.Single(x => x.Id == 2)),
                (existing.Single(x => x.Id == 3), incoming.Single(x => x.Id == 3))
            };
            Assert.Equal(expectedUpdated, matches.Updated);
            Assert.Equal(existing.Where(x => new[] { 1, 4 }.Contains(x.Id)), matches.Dropped);
            Assert.Equal(incoming.Where(x => new[] { 0, 12 }.Contains(x.Id)), matches.Added);
            Assert.Equal(3, matches.Added.Count);
        }

        [Fact]
        public void TestEntityMatcher_ComplexKey()
        {
            var existing = new[]
            {
                new Certification
                {
                    Id = 1,
                    CollegeCode = 1,
                    LicenseNumber = "11111"
                },
                new Certification
                {
                    Id = 2,
                    CollegeCode = 2,
                    LicenseNumber = "22222"
                }
            };
            var incoming = new[]
            {
                new Certification
                {
                    Id = 1,
                    CollegeCode = 3,
                    LicenseNumber = "33333"
                },
                new Certification
                {
                    Id = 0,
                    CollegeCode = 2,
                    LicenseNumber = "22222"
                }
            };

            var matches = EntityMatcher
                .MatchUsing((Certification c) => $"{c.CollegeCode},{c.LicenseNumber}")
                .Match(existing, incoming);

            Assert.Equal(new (Certification, Certification)[] { new(existing.Single(c => c.Id == 2), incoming.Single(c => c.Id == 0)) }, matches.Updated);
            Assert.Equal(existing.Where(c => c.Id == 1), matches.Dropped);
            Assert.Equal(incoming.Where(c => c.Id == 1), matches.Added);
        }

        public class Entity
        {
            public int Id { get; set; }

            public Entity(int id)
            {
                Id = id;
            }
        }

        public class ViewModel
        {
            public int Id { get; set; }

            public ViewModel(int id)
            {
                Id = id;
            }
        }
    }
}
