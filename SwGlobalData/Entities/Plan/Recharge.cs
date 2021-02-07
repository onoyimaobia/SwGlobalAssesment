using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Entities.Plan
{
    public class Recharge
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long OperatorId { get; set; }
        public long RechargeTypeId { get; set; }
        //optional if null use the registered number
        public string MobileNumber { get; set; }
        public string Description { get; set; }
        public long PlanId { get; set; }
        public int Amount { get; set; }
    }
    public class RechargeType
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
