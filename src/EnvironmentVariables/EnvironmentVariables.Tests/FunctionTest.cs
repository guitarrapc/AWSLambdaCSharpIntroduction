using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using EnvironmentVariables;

namespace EnvironmentVariables.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestEnvironmentVariable()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var value = function.FunctionHandler("", context);
            Assert.Equal("Bar", value);
        }
    }
}
