using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace GithubWebhook
{
    public class Function
    {
        private static readonly string webhookUrl = Environment.GetEnvironmentVariable("SlackWebhookUrl");

        /// <summary>
        /// Github Webhook -> SNS -> Lambda Parse (HERE!!) -> Slack Executor
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<Response> FunctionHandlerAsync(SnsRequest input, ILambdaContext context)
        {
            var snsMessage = input.Records.FirstOrDefault()?.Sns.Message;
            if (snsMessage == null)
            {
                throw new NullReferenceException(nameof(snsMessage));
            }

            var githubEvent = input.Records.FirstOrDefault()?.Sns.MessageAttributes.XGithubEvent?.Value;
            if (!githubEvent.HasValue)
            {
                throw new NullReferenceException(nameof(githubEvent));
            }

            dynamic githubWebhook = JsonConvert.DeserializeObject(input.Records.First().Sns.Message);

            context.Logger.LogLine($"GitHub WebHook triggered");

            var message = "";
            switch (githubEvent.Value)
            {
                case GithubEventKind.issue_comment:
                    {
                        // Issue Comment
                        message = $@"New GitHub issue comment posted by {githubWebhook.comment.user.login} at {githubWebhook.repository.name},
Url : {githubWebhook.comment.url}
Tite : {githubWebhook.issue.title}
-----
{githubWebhook.comment.body}";
                        break;
                    }
                case GithubEventKind.issues:
                    break;
                case GithubEventKind.pull_request_review_comment:
                    break;
                case GithubEventKind.pull_request_review:
                    break;
                case GithubEventKind.pull_request:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var payload = new
            {
                channel = "#github",
                username = "AWS Lambda Bot",
                text = message,
                icon_url = "https://d0.awsstatic.com/events/new-reinvent/reinvent_launch-page_illustration_lambda.png",
            };

            var jsonString = JsonConvert.SerializeObject(payload);
            using (var client = new HttpClient())
            {
                var res = await client.PostAsync(webhookUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));
                return new Response(res.StatusCode, $"Github webhook invoked. Message : {message}");
            }
        }
    }

    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }

        public Response(HttpStatusCode code, string body)
        {
            StatusCode = code;
            Body = body;
        }
    }
}