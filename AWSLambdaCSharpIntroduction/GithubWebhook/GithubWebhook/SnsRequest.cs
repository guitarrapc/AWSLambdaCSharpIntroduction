using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GithubWebhook
{
    public class SnsRequest
    {
        public Record[] Records { get; set; }
    }

    public class Record
    {
        public string EventVersion { get; set; }
        public string EventSubscriptionArn { get; set; }
        public string EventSource { get; set; }
        public Sns Sns { get; set; }
    }

    public class Sns
    {
        public string SignatureVersion { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; }
        public string SigningCertUrl { get; set; }
        public string MessageId { get; set; }
        public string Message { get; set; }
        public Messageattributes MessageAttributes { get; set; }
        public string Type { get; set; }
        public string UnsubscribeUrl { get; set; }
        public string TopicArn { get; set; }
        public string Subject { get; set; }
    }

    public class Messageattributes
    {
        [JsonProperty("X-Github-Event")]
        public XGithubEvent XGithubEvent { get; set; }
    }

    public class XGithubEvent
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
