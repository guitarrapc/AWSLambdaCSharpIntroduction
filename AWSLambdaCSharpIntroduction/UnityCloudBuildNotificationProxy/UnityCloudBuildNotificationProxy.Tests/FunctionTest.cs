using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using UnityCloudBuildNotificationProxy;

namespace UnityCloudBuildNotificationProxy.Tests
{
    public class FunctionTest
    {
        #region Property
        private string InvalidJson = @"{
  ""channel"": 123456,
  ""text"": ""Send from AWS Lambda""
}";
        private string PingJson => @"{
    ""body"": {
        ""hookId"": 2900
    }
}";
        private string MissingProjectJson => @"{
  ""body"": {
    ""hoge"": ""My Project""
  }
}";
        private string ValidJson => @"{
  ""body"": {
    ""projectName"": ""My Project"",
    ""buildTargetName"": ""Mac desktop 32-bit build"",
    ""projectGuid"": ""0895432b-43a2-4fd3-85f0-822d8fb607ba"",
    ""orgForeignKey"": ""13260"",
    ""buildNumber"": 14,
    ""buildStatus"": ""queued"",
    ""startedBy"": ""Build User <builduser@domain.com>"",
    ""platform"": ""standaloneosxintel"",
    ""links"": {
      ""api_self"": {
        ""method"": ""get"",
        ""href"": ""/api/orgs/my-org/projects/my-project/buildtargets/mac-desktop-32-bit-build/builds/14""
      },
      ""dashboard_url"": {
        ""method"": ""get"",
        ""href"": ""https://build.cloud.unity3d.com""
      },
      ""dashboard_project"": {
        ""method"": ""get"",
        ""href"": ""/build/orgs/stephenp/projects/assetbundle-demo-1""
      },
      ""dashboard_summary"": {
        ""method"": ""get"",
        ""href"": ""/build/orgs/my-org/projects/my-project/buildtargets/mac-desktop-32-bit-build/builds/14/summary""
      },
      ""dashboard_log"": {
        ""method"": ""get"",
        ""href"": ""/build/orgs/my-org/projects/my-project/buildtargets/mac-desktop-32-bit-build/builds/14/log""
      }
    }
  }
}";
        #endregion

        #region Tests
        [Fact]
        public async Task AssertInvalidJsonFormat()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var json = InvalidJson;
            dynamic input = JsonConvert.DeserializeObject(json);
            Assert.Throws<NullReferenceException>(() => function.FunctionHandler(input, context));

            //"Input json detected as ping. return immediately."
        }

        [Fact]
        public async Task AssertPingJsonFormat()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var json = PingJson;
            dynamic input = JsonConvert.DeserializeObject(json);
            var result = function.FunctionHandler(input, context);
            Assert.Equal("Input json detected as ping. return immediately.", result);
        }

        [Fact]
        public async Task AssertMissingProjectKeyJsonFormat()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var json = MissingProjectJson;
            dynamic input = JsonConvert.DeserializeObject(json);
            var result = function.FunctionHandler(input, context);
            Assert.Equal("Input json not detected to contain body.projectName Key. return immediately.", result);
        }

        [Fact]
        public async Task AssertValidJsonFormat()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var json = ValidJson;
            dynamic input = JsonConvert.DeserializeObject(json);
            var result = function.FunctionHandler(input, context);
            Assert.Equal("[info][title]Unity Cloud Build #14: Build queued (Started by : Build User <builduser@domain.com>)[/title]\r\nhttps://developer.cloud.unity3d.com/build/orgs/my-org/projects/my-project/buildtargets/mac-desktop-32-bit-build/builds/14/summary\r\n[title]Platform : standaloneosxintel[/title]Mac desktop 32-bit build[/info]", result);
        }
        #endregion
    }
}
