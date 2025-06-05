using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertManagement.AddAlert.AlertModels.AlertModels
{
    public class AddAlertResponse
    {
        public bool Success { get; set; }
        public string AlertId { get; set; }
        public string Message { get; set; }
    }
}
