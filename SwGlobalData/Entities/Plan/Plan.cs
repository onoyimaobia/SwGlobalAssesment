using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Entities.Plan
{
    public class Plan
    {
        public long OperatorId { get; set; }
        public long Id { get; set; }
        public long ValueAddedServiceId { get; set; }
        public int Amount { get; set; }
        public string SiteUrl { get; set; }

    }
    public class ValueAddedService
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
