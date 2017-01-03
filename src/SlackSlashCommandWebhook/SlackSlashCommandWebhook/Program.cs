using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaShared;

namespace SlackSlashCommandWebhook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = new SlackSlashCommand { Body = "token=XXXXXXXXXXXXXXXXXXXXXX&team_id=1234&team_domain=hogemoge&channel_id=1234&channel_name=hogemoge&user_id=hogemoge&user_name=hogemoge&command=%2Fnow&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2123%2F456%2F789" };
            var response = new Function().FunctionHandlerAsync(input, new LambdaContext());
        }
    }
}
