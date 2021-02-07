using SwGlobalData.DataContext;
using SwGlobalData.Entities.Plan;
using SwGlobalData.Service.DTO.Payload;
using SwGlobalData.Service.DTO.Resources;
using SwGlobalData.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwGlobalData.Service.Concrete
{
    public class OperatorService : IOperatorService
    {
        private readonly SwGlobalDbContext _db;
        public OperatorService(SwGlobalDbContext db)
        {
            this._db = db;
        }
        public async Task<GeneralResource> CreateOperator(OperatorPayload payload)
        {
            if (string.IsNullOrEmpty(payload.OperatorName) || string.IsNullOrEmpty(payload.OperatorUrl) || string.IsNullOrEmpty(payload.OperatorImage))
                return new GeneralResource { Status = false, Message = "all payloads are required" };
            var operat = new Operator
            {
                OperatorName = payload.OperatorName,
                OperatorImage = payload.OperatorImage,
                OperatorUrl = payload.OperatorUrl
            };
            _db.Operators.Add(operat);
            if (!await _db.TrySaveChanges()) return new GeneralResource { Status = false, Message = "Unable to save data to db" };
            return new GeneralResource { Status = true, Message = "success" };
        }
    }
}
