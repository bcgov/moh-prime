using System.Collections.Generic;
using System.Linq;
using Xunit;

using Prime.Models;
using Prime.Services;
using Prime.ViewModels;
using PrimeTests.Utils;
using Microsoft.EntityFrameworkCore;

namespace PrimeTests.UnitTests
{
    public class CommunitySiteServiceTests : InMemoryDbTest
    {
        [Fact]
        public async void TestUpdateSite_NewRemoteUsers()
        {
            // Arrange
            var site = new CommunitySite
            {
                Organization = new Organization { SigningAuthority = new Party { FirstName = "", LastName = "" } },
                PhysicalAddress = new PhysicalAddress(),
                Provisioner = new Party(),
                RemoteUsers = new List<RemoteUser>
                {
                    new RemoteUser
                    {
                        Id = 1,
                        FirstName = "1",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new List<RemoteUserCertification>
                        {
                            new RemoteUserCertification
                            {
                                CollegeCode = 1,
                                LicenseNumber = "11111"
                            }
                        }
                    },
                    new RemoteUser
                    {
                        Id = 2,
                        FirstName = "Dropped",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new List<RemoteUserCertification>
                        {
                            new RemoteUserCertification
                            {
                                CollegeCode = 1,
                                LicenseNumber = "Dropped"
                            }
                        }
                    }
                }
            };
            TestDb.CommunitySites.Add(site);
            TestDb.SaveChanges();

            var update = new CommunitySiteUpdateModel
            {
                Id = site.Id,
                RemoteUsers = new[]
                {
                    new RemoteUser
                    {
                        Id = 1,
                        FirstName = "Updated1",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new[]
                        {
                            new RemoteUserCertification
                            {
                                CollegeCode = 1,
                                LicenseNumber = "Updated1"
                            }
                        }
                    },
                    new RemoteUser
                    {
                        Id = 0,
                        FirstName = "Added1",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new[]
                        {
                            new RemoteUserCertification
                            {
                                CollegeCode = 1,
                                LicenseNumber = "Added1"
                            }
                        }
                    },
                    new RemoteUser
                    {
                        Id = 0,
                        FirstName = "Added2",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new RemoteUserCertification[]{}
                    },
                    new RemoteUser
                    {
                        Id = 77,
                        FirstName = "Added3",
                        LastName = "",
                        Email = "",
                        RemoteUserCertifications = new RemoteUserCertification[]{}
                    }
                }
            };

            // Act
            await MockDependenciesFor<CommunitySiteService>().UpdateSiteAsync(site.Id, update);

            // Assert
            // Drops
            Assert.False(TestDb.RemoteUsers.Any(user => user.FirstName == "Dropped"));
            Assert.False(TestDb.RemoteUserCertifications.Any(cert => cert.LicenseNumber == "Dropped"));

            var siteUsers = TestDb.RemoteUsers
                .Include(user => user.RemoteUserCertifications)
                .Where(user => user.SiteId == site.Id)
                .ToList();

            // Updates
            var updated = siteUsers.SingleOrDefault(user => user.Id == 1);
            Assert.NotNull(updated);
            Assert.Equal("Updated1", updated.FirstName);
            Assert.Equal("Updated1", updated.RemoteUserCertifications.SingleOrDefault()?.LicenseNumber);

            // Adds
            var added1 = siteUsers.SingleOrDefault(user => user.FirstName == "Added1");
            Assert.NotNull(added1);
            Assert.NotEqual(0, added1.Id);
            Assert.Equal("Added1", added1.RemoteUserCertifications.SingleOrDefault()?.LicenseNumber);

            var added2 = siteUsers.SingleOrDefault(user => user.FirstName == "Added2");
            Assert.NotNull(added2);
            Assert.NotEqual(0, added2.Id);

            var added3 = siteUsers.SingleOrDefault(user => user.FirstName == "Added3");
            Assert.NotNull(added3);
            Assert.NotEqual(0, added3.Id);
            Assert.NotEqual(77, added3.Id);
        }
    }
}
