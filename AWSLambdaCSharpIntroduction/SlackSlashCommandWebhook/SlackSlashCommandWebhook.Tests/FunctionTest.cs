using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Moq;
using SlackSlashCommandWebhook;

namespace SlackSlashCommandWebhook.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var response = function.FunctionHandlerAsync(new SlackSlashCommand(), context).Result;

            Assert.Equal("in_channel", response.ResponseType);
            Assert.Equal("Hello from Lambda .NET Core.", response.Text);
        }
    }
}
