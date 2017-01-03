using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace EnvironmentVariables
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            context.Logger.LogLine("EnvironmentVariables triggered.");

            // Add Environment Variable for Key -> Foo, Value -> Bar
            var envKey = "Foo";
            var envValue = Environment.GetEnvironmentVariable(envKey);
            context.Logger.LogLine($"Environment Variables. Key : {envKey}, Value : {envValue}");

            return envValue;
        }
    }
}
