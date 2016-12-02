using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SimpleClassFunction
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SampleClass FunctionHandler(SampleClass input, ILambdaContext context)
        {
            var response = new SampleClass
            {
                Str = input.Str?.ToUpper(),
                StrArray = input.StrArray.Select(x => x?.ToUpper()).ToArray(),
            };
            return response;
        }
    }

    public class SampleClass
    {
        public string Str { get; set; }
        public string[] StrArray { get; set; }
    }
}