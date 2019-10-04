using System.Security.Claims;
using System.Linq;
using Bogus;
using Prime.Models;
using Prime;
using Prime.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace PrimeTests.Utils
{
    public class TestUtils
    {
        public static Faker<PhysicalAddress> PhysicalAddressFaker = new Faker<PhysicalAddress>()
                                .RuleFor(a => a.Country, f => f.Address.Country())
                                .RuleFor(a => a.Province, f => f.Address.StateAbbr())
                                .RuleFor(a => a.Street, f => f.Address.StreetName())
                                .RuleFor(a => a.City, f => f.Address.City())
                                .RuleFor(a => a.Postal, f => f.Address.ZipCode())
                                ;

        public static Faker<MailingAddress> MailingAddressFaker = new Faker<MailingAddress>()
                                .RuleFor(a => a.Country, f => f.Address.Country())
                                .RuleFor(a => a.Province, f => f.Address.StateAbbr())
                                .RuleFor(a => a.Street, f => f.Address.StreetName())
                                .RuleFor(a => a.City, f => f.Address.City())
                                .RuleFor(a => a.Postal, f => f.Address.ZipCode())
                                ;

        public static Faker<Enrollee> EnroleeFaker = new Faker<Enrollee>()
                                .RuleFor(e => e.UserId, f => f.Random.Word())
                                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                                .RuleFor(e => e.MiddleName, f => f.Name.FirstName())
                                .RuleFor(e => e.LastName, f => f.Name.LastName())
                                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(20))
                                .RuleFor(e => e.PhysicalAddress, PhysicalAddressFaker.Generate())
                                .RuleFor(e => e.MailingAddress, MailingAddressFaker.Generate())
                                ;

        public static Faker<Enrolment> EnrolmentFaker = new Faker<Enrolment>()
                                    .RuleFor(e => e.Enrollee, EnroleeFaker.Generate())
                                    .RuleFor(e => e.HasCertification, f => f.Random.Bool())
                                    .RuleFor(e => e.IsDeviceProvider, f => f.Random.Bool())
                                    .RuleFor(e => e.IsInsulinPumpProvider, f => f.Random.Bool())
                                    .RuleFor(e => e.IsAccessingPharmaNetOnBehalfOf, f => f.Random.Bool())
                                    .RuleFor(e => e.HasConviction, f => f.Random.Bool())
                                    .RuleFor(e => e.HasRegistrationSuspended, f => f.Random.Bool())
                                    .RuleFor(e => e.HasDisciplinaryAction, f => f.Random.Bool())
                                    .RuleFor(e => e.HasPharmaNetSuspended, f => f.Random.Bool())
                                    ;

        public static int? CreateEnrolment(ApiDbContext apiDbContext)
        {
            var enrolment = TestUtils.EnrolmentFaker.Generate();
            return new DefaultEnrolmentService(apiDbContext).CreateEnrolmentAsync(enrolment).Result;
        }

        public static Enrolment GetEnrolmentById(ApiDbContext apiDbContext, int enrolmentId)
        {
            return new DefaultEnrolmentService(apiDbContext).GetEnrolmentAsync(enrolmentId).Result;
        }
    }
}