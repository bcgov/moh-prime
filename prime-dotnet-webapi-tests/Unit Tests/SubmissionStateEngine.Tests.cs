using System;
using System.Linq;
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
            foreach (EnrolleeStatusAction action in Enum.GetValues(typeof(EnrolleeStatusAction)))
            {
                yield return new object[] { action };
            }
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Editable(EnrolleeStatusAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Editable, 0);
            var expectedAllowed = new[]
            {
                EnrolleeStatusAction.LockProfile,
                EnrolleeStatusAction.DeclineProfile
            }.Contains(action);

            // Act
            var allowed = EnrolleeStatusStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_UnderReview(EnrolleeStatusAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.UnderReview, 0);
            var expectedAllowed = new[]
            {
                EnrolleeStatusAction.Approve,
                EnrolleeStatusAction.EnableEditing,
                EnrolleeStatusAction.LockProfile,
                EnrolleeStatusAction.DeclineProfile,
                EnrolleeStatusAction.RerunRules
            }.Contains(action);

            // Act
            var allowed = EnrolleeStatusStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_RequiresToa(EnrolleeStatusAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.RequiresToa, 0);
            var expectedAllowed = new[]
            {
                EnrolleeStatusAction.AcceptToa,
                EnrolleeStatusAction.DeclineToa,
                EnrolleeStatusAction.EnableEditing,
                EnrolleeStatusAction.LockProfile,
                EnrolleeStatusAction.DeclineProfile,
                EnrolleeStatusAction.CancelToaAssignment
            }.Contains(action);

            // Act
            var allowed = EnrolleeStatusStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Locked(EnrolleeStatusAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Locked, 0);
            var expectedAllowed = new[]
            {
                EnrolleeStatusAction.EnableEditing,
                EnrolleeStatusAction.DeclineProfile
            }.Contains(action);

            // Act
            var allowed = EnrolleeStatusStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Declined(EnrolleeStatusAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Declined, 0);
            var expectedAllowed = new[]
            {
                EnrolleeStatusAction.EnableEditing
            }.Contains(action);

            // Act
            var allowed = EnrolleeStatusStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }
    }
}
