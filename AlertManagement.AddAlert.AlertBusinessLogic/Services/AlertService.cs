using AlertManagement.AddAlert.AlertBusinessLogic.Interfaces;
using AlertManagement.AddAlert.AlertModels;
using AlertManagement.AddAlert.AlertModels.AlertModels;
using System;
using System.Threading.Tasks;

namespace AlertManagement.AddAlert.AlertBusinessLogic.Services
{
    public class AlertService : IAlertService
    {
        public Task<AddAlertResponse> AddAlertAsync(AddAlertRequest request)
        {
            // TODO: DB check
            // TODO: add alert to queue

            return Task.FromResult(new AddAlertResponse
            {
                Success = true,
                AlertId = Guid.NewGuid().ToString(),
                Message = "Alert added successfully"
            });
        }
    }
}
