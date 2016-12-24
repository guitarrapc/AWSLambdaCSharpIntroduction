using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using SlackSlashCommandWebhook;

namespace SlackSlashCommandWebhook.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void SlashCommandTest()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var input = @"{
  ""body"": ""token=XXXXXXXXXXXXXXXXXXXXXX&team_id=1234&team_domain=hogemoge&channel_id=1234&channel_name=hogemoge&user_id=hogemoge&user_name=hogemoge&command=%2Fnow&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2123%2F456%2F789""
}";
            var command = JsonConvert.DeserializeObject<SlackSlashCommand>(input);
            var response = function.FunctionHandlerAsync(command, context).Result;

            Assert.Equal("in_channel", response.ResponseType);
            Assert.Equal("Hello from Lambda .NET Core.", response.Text);
        }
    }
}
