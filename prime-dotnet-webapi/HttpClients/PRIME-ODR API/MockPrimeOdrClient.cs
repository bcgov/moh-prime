using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.HttpClients
{
    public class MockPrimeOdrClient : IPrimeOdrClient
    {
        private readonly Random randomizer = new Random();


        public Task<(List<PharmanetTransactionLog> Logs, bool ExistsMoreLogs)> RetrieveLatestPharmanetTxLogsAsync(long lastKnownTxId)
        {
            var results = new List<PharmanetTransactionLog>
                {
                    new PharmanetTransactionLog {
                        TransactionId = ++lastKnownTxId,
                        PractitionerId = "12345",
                        CollegePrefix = "91",
                        UserId = "SDFERY#$%DFGBDF@#%$SSS@@!@$",
                        TxDateTime = DateTime.Now,
                        PharmacyId = "ABC" },
                    new PharmanetTransactionLog {
                        TransactionId = ++lastKnownTxId,
                        PractitionerId = "678901",
                        CollegePrefix = "P1",
                        UserId = "HJK%^*^&*IGFHRTYRTYETREGSDF",
                        TxDateTime = DateTime.Now,
                        PharmacyId = "DEF" }
                };
            var noResults = new List<PharmanetTransactionLog>();

            // Randomly determine what is value of ExistsMoreLogs
            bool hasResults = randomizer.Next(10) != 1;
            return Task.FromResult((hasResults ? results : noResults, hasResults));
        }
    }
}
