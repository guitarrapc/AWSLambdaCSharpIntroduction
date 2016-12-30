using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace UnityCloudBuildNotificationProxy
{
    public class Function
    {
        private const string ChatworkFunctionName = "SendToChatwork";

        private static readonly string channel = Environment.GetEnvironmentVariable("ChatworkChannel");
        private static string Region => Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION");
#if DEBUG
        private static string AccessKey => Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        private static string SecretAccessKey => Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
        private static AmazonLambdaClient LambdaClient => new AmazonLambdaClient(AccessKey, SecretAccessKey, Amazon.RegionEndpoint.GetBySystemName(Region));
#elif RELEASE
        private static AmazonLambdaClient LambdaClient => new AmazonLambdaClient(Amazon.RegionEndpoint.GetBySystemName(Region));
#endif

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(dynamic input, ILambdaContext context)
        {
            // Check json input string.
            context.Logger.LogLine("UnityCloudBuildNotificationProxy webhook started!");
            context.Logger.LogLine(Region);
            var rawJson = input.ToString();
            context.Logger.LogLine(rawJson);

            if (input.body == null)
            {
                context.Logger.LogLine("Input json detected not contain body key. return immediately.");
                throw new NullReferenceException(nameof(input.body));
            }

            if (input.body.hookId != null)
            {
                var m = "Input json detected as ping. return immediately.";
                context.Logger.LogLine(m);
                return m;
            }

            if (input.body.projectName == null)
            {
                var m = "Input json not detected to contain body.projectName Key. return immediately.";
                context.Logger.LogLine(m);
                return m;
            }

            var channelId = 0;
            if (!int.TryParse(channel, out channelId))
            {
                var m = "Missing environment variable ChatworkRoomId";
                context.Logger.LogLine(m);
                throw new NullReferenceException(m);
            }

            var data = JsonConvert.DeserializeObject<UnityCloudBuildWebhook>(rawJson);
            var message = $@"[info][title]Unity Cloud Build #{data.body.buildNumber}: Build {data.body.buildStatus}[/title]Platform : {data.body.platform}
{data.body.buildTargetName}
[hr]https://developer.cloud.unity3d.com{data.body.links.dashboard_summary.href}
Started by : {data.body.startedBy}[/info]";

            var notification = new ChatworkNotification
            {
                Channel = channelId,
                Text = message,
            };

            // Pass to SendToChatwork Lambda function when Chatwork recover from Maintenance.
            var result = LambdaClient.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest
            {
                FunctionName = ChatworkFunctionName,
                Payload = JsonConvert.SerializeObject(notification),
            }).Result;
            context.Logger.LogLine(result.HttpStatusCode.ToString());
            return message;
        }
    }
}
