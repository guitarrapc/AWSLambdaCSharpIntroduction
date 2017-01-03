using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LambdaShared;

namespace EnvironmentVariables
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var response = new Function().FunctionHandler("", new LambdaContext());
            Console.WriteLine(response);
        }
    }
}
