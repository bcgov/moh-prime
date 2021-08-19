using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.HttpClients
{
    public class MockPrimeOdrClient : IPrimeOdrClient
    {
        private static int txIdCounter = 1;

        private Random randomizer = new Random();


        public Task<(List<PharmanetTransactionLog> Logs, bool ExistsMoreLogs)> RetrieveLatestPharmanetTxLogsAsync()
        {
            var results = new List<PharmanetTransactionLog>
                {
                    new PharmanetTransactionLog {
                        TransactionId = txIdCounter++,
                        PractitionerId = "12345",
                        CollegePrefix = "91",
                        UserId = "SDFERY#$%DFGBDF@#%$SSS@@!@$",
                        TxDateTime = DateTime.Now,
                        PharmacyId = "ABC" },
                    new PharmanetTransactionLog {
                        TransactionId = txIdCounter++,
                        PractitionerId = "678901",
                        CollegePrefix = "P1",
                        UserId = "HJK%^*^&*IGFHRTYRTYETREGSDF",
                        TxDateTime = DateTime.Now,
                        PharmacyId = "DEF" }
                };
            var noResults = new List<PharmanetTransactionLog>();

            // Randomly determine whether to return results or not, and what is value of ExistsMoreLogs
            return Task.FromResult((randomizer.Next(2) == 1 ? results : noResults, randomizer.Next(2) == 1));
        }
    }
}
