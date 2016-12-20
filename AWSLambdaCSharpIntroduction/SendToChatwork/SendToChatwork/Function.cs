using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatwork.Service;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SendToChatwork
{
    public class Function
    {
        private readonly string chatworkApiKey = Environment.GetEnvironmentVariable("ChatworkApiKey");

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<int> FunctionHandler(ChatworkInput input, ILambdaContext context)
        {
            context.Logger.LogLine($"{nameof(SendToChatwork)} was triggered");

            if (input.Channel == 0) throw new ArgumentOutOfRangeException(nameof(input.Channel));
            if (string.IsNullOrWhiteSpace(input.Text)) throw new ArgumentOutOfRangeException(nameof(input.Text));

            if (string.IsNullOrWhiteSpace(chatworkApiKey)) throw new NullReferenceException(nameof(chatworkApiKey));

            var roomId = input.Channel;
            var body = input.Text;

            var client = new ChatworkClient(chatworkApiKey);
            var response = await client.Room.SendMessgesAsync(roomId, body);
            return response.message_id;
        }
    }

    public class ChatworkInput
    {
        [JsonProperty("channel")]
        public int Channel { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
