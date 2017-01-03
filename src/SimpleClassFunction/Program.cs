using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LambdaShared;

namespace SimpleClassFunction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new SampleClass
            {
                Str = "hogehoge",
                StrArray = new[] { "hoge", "huga", "foo" },
            };

            var response = new Function().FunctionHandler(test, new LambdaContext());
        }
    }
}
