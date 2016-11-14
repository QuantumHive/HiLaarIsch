using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuantumHive.Core;

namespace HiLaarIsch
{
    public class HiLaarIschSettings
    {
        public HiLaarIschSettings(
            string connectionString,
            string sendGridApiKey,
            string fromMailAddress,
            string testMailAddress,
            ApplicationPhase phase)
        {
            this.ConnectionString = connectionString;
            this.ApiKeys = new ApiKey(sendGridApiKey);
            this.EmailAddresses = new EmailAddress(fromMailAddress, testMailAddress);
            this.ApplicationPhase = phase;
        }

        public string ConnectionString { get; }
        public EmailAddress EmailAddresses { get; }
        public ApiKey ApiKeys { get; }
        public ApplicationPhase ApplicationPhase { get; }

        public class ApiKey
        {
            public ApiKey(string sendGrid)
            {
                this.SendGrid = sendGrid;
            }

            public string SendGrid { get; }
        }

        public class EmailAddress
        {
            public EmailAddress(string from, string test)
            {
                this.From = from;
                this.Test = test;
            }

            public string From { get; }
            public string Test { get; }
        }
    }
}