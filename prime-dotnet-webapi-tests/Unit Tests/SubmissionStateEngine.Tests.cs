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
            foreach (SubmissionAction action in Enum.GetValues(typeof(SubmissionAction)))
            {
                yield return new object[] { action };
            }
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Editable(SubmissionAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Editable, 0);
            var expectedAllowed = new[]
            {
                SubmissionAction.LockProfile,
                SubmissionAction.DeclineProfile
            }.Contains(action);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_UnderReview(SubmissionAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.UnderReview, 0);
            var expectedAllowed = new[]
            {
                SubmissionAction.Approve,
                SubmissionAction.EnableEditing,
                SubmissionAction.LockProfile,
                SubmissionAction.DeclineProfile,
                SubmissionAction.RerunRules
            }.Contains(action);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_RequiresToa(SubmissionAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.RequiresToa, 0);
            var expectedAllowed = new[]
            {
                SubmissionAction.AcceptToa,
                SubmissionAction.DeclineToa,
                SubmissionAction.EnableEditing,
                SubmissionAction.LockProfile,
                SubmissionAction.DeclineProfile
            }.Contains(action);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Locked(SubmissionAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Locked, 0);
            var expectedAllowed = new[]
            {
                SubmissionAction.EnableEditing,
                SubmissionAction.DeclineProfile
            }.Contains(action);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }

        [Theory]
        [MemberData(nameof(SubmissionActionsData))]
        public void TestAllowableActions_Declined(SubmissionAction action)
        {
            // Arrange
            var status = EnrolmentStatus.FromType(StatusType.Declined, 0);
            var expectedAllowed = new[]
            {
                SubmissionAction.EnableEditing
            }.Contains(action);

            // Act
            var allowed = SubmissionStateEngine.AllowableAction(action, status);

            // Assert
            Assert.Equal(expectedAllowed, allowed);
        }
    }
}
