using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityCloudBuildNotificationProxy
{
    public class ChatworkNotification
    {
        [JsonProperty("channel")]
        public int Channel { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
