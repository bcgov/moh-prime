using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.Engines;
using Prime.Models.Api;

namespace PrimeTests.UnitTests
{
    public class SiteStatusStateEngineTests
    {
        public static IEnumerable<object[]> SiteStatussData()
        {
            foreach (EnrolleeStatusAction action in Enum.GetValues(typeof(SiteStatusType)))
            {
                yield return new object[] { action };
            }
        }

        [Theory]
        [MemberData(nameof(SiteStatussData))]
        public void TestAllowableStatusChange_Active(SiteStatusType newStatus)
        {
            // Arrange
            var currentStatus = SiteStatusType.Active;
            var expectedAllowed = new[]
            {
                SiteStatusType.InReview,
                SiteStatusType.Locked
            }.Contains(newStatus);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteStatussData))]
        public void TestAllowableStatusChange_InReview(SiteStatusType newStatus)
        {
            // Arrange
            var currentStatus = SiteStatusType.InReview;
            var expectedAllowed = new[]
            {
                SiteStatusType.Active,
                SiteStatusType.Approved,
                SiteStatusType.Locked
            }.Contains(newStatus);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteStatussData))]
        public void TestAllowableStatusChange_Approved(SiteStatusType newStatus)
        {
            // Arrange
            var currentStatus = SiteStatusType.Approved;
            var expectedAllowed = new[]
            {
                SiteStatusType.Active,
            }.Contains(newStatus);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteStatussData))]
        public void TestAllowableStatusChange_Locked(SiteStatusType newStatus)
        {
            // Arrange
            var currentStatus = SiteStatusType.Locked;
            var expectedAllowed = new[]
            {
                SiteStatusType.InReview
            }.Contains(newStatus);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(currentStatus, newStatus);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }
    }
}
