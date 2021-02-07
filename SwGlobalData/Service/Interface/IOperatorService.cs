using SwGlobalData.Service.DTO.Payload;
using SwGlobalData.Service.DTO.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Service.Interface
{
    public interface IOperatorService
    {
        Task<GeneralResource> CreateOperator(OperatorPayload payload);
    }
}
