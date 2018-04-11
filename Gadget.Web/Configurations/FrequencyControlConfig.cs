using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gadget.Web.Configurations
{
    public class FrequencyControlConfig
    {
        public UsagePlan[] UsagePlans { get; set; }

        public ApiUsagePlanMapping[] ApiUsagePlanMappings { get; set; }
    }

    public class UsagePlan
    {
        public string UsagePlanName { get; set; }
        public int RatePerSecond { get; set; }
        public int Burst { get; set; }
        public int RequestPerDay { get; set; }
    }

    public class ApiUsagePlanMapping
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UsagePlanName { get; set; }
    }
}
