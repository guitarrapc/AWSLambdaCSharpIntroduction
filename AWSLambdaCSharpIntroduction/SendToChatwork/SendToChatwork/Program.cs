using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaShared;

namespace SendToChatwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = new ChatworkInput
            {
                Channel = int.Parse(Environment.GetEnvironmentVariable("ChatworkChannel")),
                Text = "Send from AWS Lambda"
            };

            // Local Async Execution will fail with NullReference Exception when using Logger.
            var responseTask = new Function().FunctionHandler(input, new LambdaContext());
            var response = responseTask.Result;
            Console.WriteLine(response);
        }
    }
}
