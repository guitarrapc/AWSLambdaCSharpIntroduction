using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using SimpleAsyncFunction;

namespace SimpleAsyncFunction.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = await function.FunctionHandlerAsync("hello world", context);

            Assert.Equal("HELLO WORLD", upperCase);
        }

        [Fact]
        public async Task TestToLowerFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = await function.FunctionHandlerAsync("hello world", context);

            Assert.NotEqual("hello world", upperCase);
        }

        [Fact]
        public async Task TestShouldFailFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = await function.FunctionHandlerAsync("hello world", context);

            Assert.NotEqual("hogemoge", upperCase);
        }
    }
}
