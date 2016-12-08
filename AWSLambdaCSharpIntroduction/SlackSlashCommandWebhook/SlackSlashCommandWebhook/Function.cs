using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SlackSlashCommandWebhook
{
    public class Function
    {
        private static readonly string slackTokenEnvironmentKey = "SlackToken";
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<Result> FunctionHandlerAsync(SlackSlashCommand input, ILambdaContext context)
        {
            // Parse to result
            var split = input.Body.Split('&');
            if (split.Length != 10) throw new ArgumentOutOfRangeException(nameof(split));
            var result = new ParseResult(split);

            // Authentication Validation
            ValidateToken(result.Token);

            // For Test purpose. You can check what was request strings.
            //return new Result("in_channel", input.Body);

            // Only you can see response when you change in_channel -> ephemeral.
            return new Result("in_channel", "Hello from Lambda .NET Core.");
        }

        private void ValidateToken(string token)
        {
            // Get Valid Token from AWS Lambda Environment Variable
            var validToken = Environment.GetEnvironmentVariable(slackTokenEnvironmentKey);
            if (string.IsNullOrWhiteSpace(validToken)) throw new NullReferenceException(slackTokenEnvironmentKey);

            // Validate
            if (token == validToken) return;
            throw new UnauthorizedAccessException();
        }
    }

    /// <summary>
    /// Slack SlashCommand Recieve Type
    /// </summary>
    public class SlackSlashCommand
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }

    /// <summary>
    /// Slack Slash Command Response Type
    /// </summary>
    public class Result
    {
        [JsonProperty("response_type")]
        public string ResponseType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public Result(string response, string text)
        {
            ResponseType = response;
            Text = text;
        }
    }

    /// <summary>
    /// Slack Slash Command Parse Class
    /// </summary>
    public class ParseResult
    {
        public string Token { get; set; }
        public string TeamId { get; set; }
        public string TeamDomain { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Command { get; set; }
        public string Text { get; set; }
        public string ResponseUrl { get; set; }

        public ParseResult(string[] splitArray)
        {
            Token = Parse(splitArray[0]);
            TeamId = Parse(splitArray[1]);
            TeamDomain = Parse(splitArray[2]);
            ChannelId = Parse(splitArray[3]);
            ChannelName = Parse(splitArray[4]);
            UserId = Parse(splitArray[5]);
            UserName = Parse(splitArray[6]);
            Command = Parse(splitArray[7]);
            Text = Parse(splitArray[8]);
            ResponseUrl = Parse(splitArray[9]);
        }

        private string Parse(string txt)
        {
            return txt.Split('=').Last();
        }
    }
}