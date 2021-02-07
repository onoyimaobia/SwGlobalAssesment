using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Entities.Plan
{
    public class Operator
    {
        public long Id { get; set; }
        public string OperatorName { get; set; }
        public string OperatorUrl { get; set; }
        public string OperatorImage { get; set; }
    }
}
