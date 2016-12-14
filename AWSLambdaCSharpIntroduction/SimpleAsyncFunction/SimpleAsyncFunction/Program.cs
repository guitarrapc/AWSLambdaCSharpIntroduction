using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAsyncFunction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Local Async Execution will fail with NullReference Exception when using Logger.
            var responseTask = new Function().FunctionHandlerAsync("hoge", new LambdaContext());
            var response = responseTask.Result;
            Console.WriteLine(response);
        }
    }
}
