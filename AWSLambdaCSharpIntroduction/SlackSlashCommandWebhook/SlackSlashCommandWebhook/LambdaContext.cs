using System;
using Amazon.Lambda.Core;

namespace SlackSlashCommandWebhook
{
    /// <summary>
    /// Implementation for Local Debug
    /// </summary>
    public class LambdaContext : ILambdaContext
    {
        #region Implementation of ILambdaContext

        public string AwsRequestId { get; }
        public IClientContext ClientContext { get; }
        public string FunctionName { get; } = typeof(LambdaContext).Namespace;
        public string FunctionVersion { get; }
        public ICognitoIdentity Identity { get; }
        public string InvokedFunctionArn { get; }
        public ILambdaLogger Logger { get; } = new CustomLambdaLogger();
        public string LogGroupName { get; }
        public string LogStreamName { get; }
        public int MemoryLimitInMB { get; }
        public TimeSpan RemainingTime { get; }

        #endregion
    }

    /// <summary>
    /// Implementation for Local Logger
    /// </summary>
    public class CustomLambdaLogger : ILambdaLogger
    {
        #region Implementation of ILambdaLogger

        public void Log(string message)
        {
            Console.Write(message);
        }

        #endregion

        #region Implementation of ILambdaLogger

        public void LogLine(string message)
        {
            Console.WriteLine(message);
        }

        #endregion
    }
}
