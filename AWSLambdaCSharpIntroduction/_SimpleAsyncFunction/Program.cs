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
            var responseTask = new Function().FunctionHandlerAsync("hoge", null);
            var response = responseTask.Result;
            Console.WriteLine(response);
        }
    }
}
