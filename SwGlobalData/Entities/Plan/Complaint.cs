using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Entities.Plan
{
    public class Complaint
    {
        public int Id { get; set; }
        public ComplaintType ComplaintType { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
    }
     public enum ComplaintType{
        Hard, Mild, Minor
    }
}
