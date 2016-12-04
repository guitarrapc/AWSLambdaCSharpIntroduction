using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SimpleAsyncFunction
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandlerAsync(string input, ILambdaContext context)
        {
            // Local Async Execution will fail with NullReference Exception when using Logger.
            // For more detail about Logger refer : https://docs.aws.amazon.com/ja_jp/lambda/latest/dg/dotnet-logging.html
            await Task.WhenAll(
                ConsoleLoggerHandler(context),
                LambdaLoggerHandler(context),
                ContextLoggerHandler(context));

            // You can check where these log will be written with LogGroup or StreamName in ILambdaContext
            context.Logger.LogLine($"Logs will be write to following. {nameof(context.LogGroupName)} : {context.LogGroupName}, {nameof(context.LogStreamName)} : {context.LogStreamName}");

            return input.ToUpper();
        }

        /// <summary>
        /// Any Standard Out/Error will be logged in CloudWatch Logs. But not to Lambda Log history.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ConsoleLoggerHandler(ILambdaContext context)
        {
            Console.WriteLine("[Console] Function name: " + context.FunctionName);
            Console.WriteLine("[Console] RemainingTime: " + context.RemainingTime);
            await Task.Delay(TimeSpan.FromSeconds(0.42));
            Console.WriteLine("[Console] RemainingTime: " + context.RemainingTime);
        }

        /// <summary>
        /// Will be logged to both CloudWatch Logs and Lambda History. But no new lines.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task LambdaLoggerHandler(ILambdaContext context)
        {
            LambdaLogger.Log("[LambdaLogger] Function name: " + context.FunctionName);
            LambdaLogger.Log("[LambdaLogger] RemainingTime: " + context.RemainingTime);
            await Task.Delay(TimeSpan.FromSeconds(0.42));
            LambdaLogger.Log("[LambdaLogger] RemainingTime: " + context.RemainingTime);
        }

        /// <summary>
        /// Will be logged to both CloudWatch Logs and Lambda History. You can select Log or LogLine.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ContextLoggerHandler(ILambdaContext context)
        {
            context.Logger.LogLine("[ContextLogger] Function name: " + context.FunctionName);
            context.Logger.LogLine("[ContextLogger] RemainingTime: " + context.RemainingTime);
            await Task.Delay(TimeSpan.FromSeconds(0.42));
            context.Logger.LogLine("[ContextLogger] RemainingTime: " + context.RemainingTime);
        }
    }
}
