using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace SendToChatwork.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestSucessFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();

            var input = new ChatworkInput
            {
                Channel = int.Parse(Environment.GetEnvironmentVariable("ChatworkChannel")),
                Text = "Send from AWS Lambda"
            };
            Console.WriteLine(input.Channel);
            var result = await function.FunctionHandler(input, context);

            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task TestDetectInvalidChannelFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();

            var input = new ChatworkInput
            {
                Channel = 0,
                Text = "Send from AWS Lambda"
            };
            Assert.Throws<AggregateException>(() => function.FunctionHandler(input, context).Result);
        }

        [Fact]
        public async Task TestDetectInvalidTextFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();

            var input = new ChatworkInput
            {
                Channel = int.Parse(Environment.GetEnvironmentVariable("ChatworkChannel")),
                Text = ""
            };
            Assert.Throws<AggregateException>(() => function.FunctionHandler(input, context).Result);
        }
    }
}
