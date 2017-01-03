using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnityCloudBuildNotificationProxy
{
    public class UnityCloudBuildWebhook
    {
        public Body body { get; set; }
    }

    public class Body
    {
        public string projectName { get; set; }
        public string buildTargetName { get; set; }
        public string projectGuid { get; set; }
        public string orgForeignKey { get; set; }
        public int buildNumber { get; set; }
        public string buildStatus { get; set; }
        public string lastBuiltRevision { get; set; }
        public string startedBy { get; set; }
        public string platform { get; set; }
        public string scmType { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public Api_Self api_self { get; set; }
        public Dashboard_Url dashboard_url { get; set; }
        public Dashboard_Project dashboard_project { get; set; }
        public Dashboard_Summary dashboard_summary { get; set; }
        public Dashboard_Log dashboard_log { get; set; }
    }

    public class Api_Self
    {
        public string method { get; set; }
        public string href { get; set; }
    }

    public class Dashboard_Url
    {
        public string method { get; set; }
        public string href { get; set; }
    }

    public class Dashboard_Project
    {
        public string method { get; set; }
        public string href { get; set; }
    }

    public class Dashboard_Summary
    {
        public string method { get; set; }
        public string href { get; set; }
    }

    public class Dashboard_Log
    {
        public string method { get; set; }
        public string href { get; set; }
    }
}
