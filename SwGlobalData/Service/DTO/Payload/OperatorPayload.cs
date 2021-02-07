using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Service.DTO.Payload
{
    public class OperatorPayload
    {
        [Required]
        public string OperatorName { get; set; }
        [Required]
        public string OperatorUrl { get; set; }
        
        public string OperatorImage { get; set; }
    }
}
