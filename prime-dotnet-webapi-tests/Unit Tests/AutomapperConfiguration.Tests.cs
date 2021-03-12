using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using Prime.Services.EmailInternal;
using PrimeTests.Utils;
using Prime.HttpClients;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;
using PrimeTests.ModelFactories;
using Prime.Infrastructure.AutoMapperProfiles;

using AutoMapper;

namespace PrimeTests.Unit_Tests
{
    public class AutomapperConfigurationTests
    {
        [Fact]
        public void SharedConfiguration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SharedMappingConfigurations>());
            config.AssertConfigurationIsValid();
        }
    }
}
