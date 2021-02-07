using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Entities.Plan
{
    public class Offer
    {
        public long Id { get; set; }
        public long OperatorId { get; set; }
        public string OfferImageUrl { get; set; }
        public string Description  { get; set; }
    }
}
