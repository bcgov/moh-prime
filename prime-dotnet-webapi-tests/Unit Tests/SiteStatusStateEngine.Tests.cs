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
        public static IEnumerable<object[]> SiteRegistrationActionData()
        {
            foreach (EnrolleeStatusAction action in Enum.GetValues(typeof(SiteRegistrationAction)))
            {
                yield return new object[] { action };
            }
        }

        [Theory]
        [MemberData(nameof(SiteRegistrationActionData))]
        public void TestAllowableStatusChange_Active(SiteRegistrationAction action)
        {
            // Arrange
            var expectedAllowed = new[]
            {
                SiteRegistrationAction.Submit,
                SiteRegistrationAction.Decline
            }.Contains(action);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(action, SiteStatusType.Active);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteRegistrationActionData))]
        public void TestAllowableStatusChange_InReview(SiteRegistrationAction action)
        {
            // Arrange
            var expectedAllowed = new[]
            {
                SiteRegistrationAction.Approve,
                SiteRegistrationAction.Decline,
                SiteRegistrationAction.RequestChange
            }.Contains(action);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(action, SiteStatusType.InReview);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteRegistrationActionData))]
        public void TestAllowableStatusChange_Approved(SiteRegistrationAction action)
        {
            // Arrange
            var expectedAllowed = new[]
            {
                SiteRegistrationAction.Unapprove,

            }.Contains(action);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(action, SiteStatusType.Approved);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SiteRegistrationActionData))]
        public void TestAllowableStatusChange_Locked(SiteRegistrationAction action)
        {
            // Arrange
            var expectedAllowed = new[]
            {
                SiteRegistrationAction.Undecline,
            }.Contains(action);

            // Act
            var allowed = SiteStatusStateEngine.AllowableStatusChange(action, SiteStatusType.Locked);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }
    }
}
