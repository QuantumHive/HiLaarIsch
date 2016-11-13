using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiLaarIsch
{
    public class HiLaarIschSettings
    {
        public HiLaarIschSettings(
            string connectionString,
            string sendGridApiKey,
            string fromMailAddress)
        {
            this.ConnectionString = connectionString;
            this.ApiKeys = new ApiKey(sendGrid: sendGridApiKey);
            this.EmailAddresses = new EmailAddress(from: fromMailAddress);
        }

        public string ConnectionString { get; }
        public EmailAddress EmailAddresses { get; }
        public ApiKey ApiKeys { get; }

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
            public EmailAddress(string from)
            {
                this.From = from;
            }

            public string From { get; }
        }
    }
}