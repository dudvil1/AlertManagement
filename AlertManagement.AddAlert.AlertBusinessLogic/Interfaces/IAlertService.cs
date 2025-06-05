using AlertManagement.AddAlert.AlertModels.AlertModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertManagement.AddAlert.AlertBusinessLogic.Interfaces
{
    public interface IAlertService
    {
        Task<AddAlertResponse> AddAlertAsync(AddAlertRequest request);
    }
}
