using System;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.Engines;
using Prime.Models.Api;

namespace PrimeTests.UnitTests
{
    public class SubmissionStateEngineTests
    {
        public static IEnumerable<object[]> SubmissionActionsData()
        {
            foreach (SubmissionAction action in Enum.GetValues(typeof(SubmissionAction)))
            {
                yield return new object[] { action, true };
                yield return new object[] { action, false };
            }
        }

        private Enrollee EnrolleeWithCurrentStatus(StatusType type)
        {
            return new Enrollee
            {
                EnrolmentStatuses = new[]
                {
                    EnrolmentStatus.FromType(type, 0)
                }
            };
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Editable(SubmissionAction action, bool asAdmin)
        {
            // Arrange
            var enrollee = EnrolleeWithCurrentStatus(StatusType.Editable);
            var expected = (action == SubmissionAction.LockProfile && asAdmin)
                || (action == SubmissionAction.DeclineProfile && asAdmin);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, enrollee, asAdmin);

            // Assert
            Assert.Equal(expected, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_UnderReview(SubmissionAction action, bool asAdmin)
        {
            // Arrange
            var enrollee = EnrolleeWithCurrentStatus(StatusType.UnderReview);
            var expected = (action == SubmissionAction.Approve && asAdmin)
                || (action == SubmissionAction.EnableEditing && asAdmin)
                || (action == SubmissionAction.LockProfile && asAdmin)
                || (action == SubmissionAction.DeclineProfile && asAdmin)
                || (action == SubmissionAction.RerunRules && asAdmin);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, enrollee, asAdmin);

            // Assert
            Assert.Equal(expected, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_RequiresToa(SubmissionAction action, bool asAdmin)
        {
            // Arrange
            var enrollee = EnrolleeWithCurrentStatus(StatusType.RequiresToa);
            var expected = (action == SubmissionAction.AcceptToa && !asAdmin)
                || (action == SubmissionAction.DeclineToa && !asAdmin)
                || (action == SubmissionAction.EnableEditing && asAdmin)
                || (action == SubmissionAction.LockProfile && asAdmin)
                || (action == SubmissionAction.DeclineProfile && asAdmin);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, enrollee, asAdmin);

            // Assert
            Assert.Equal(expected, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Locked(SubmissionAction action, bool asAdmin)
        {
            // Arrange
            var enrollee = EnrolleeWithCurrentStatus(StatusType.Locked);
            var expected = (action == SubmissionAction.EnableEditing && asAdmin)
                || (action == SubmissionAction.DeclineProfile && asAdmin);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, enrollee, asAdmin);

            // Assert
            Assert.Equal(expected, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Declined(SubmissionAction action, bool asAdmin)
        {
            // Arrange
            var enrollee = EnrolleeWithCurrentStatus(StatusType.Declined);
            var expected = (action == SubmissionAction.EnableEditing && asAdmin);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, enrollee, asAdmin);

            // Assert
            Assert.Equal(expected, allowed);
        }
    }
}
