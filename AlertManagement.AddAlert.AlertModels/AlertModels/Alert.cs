using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertManagement.AddAlert.AlertModels.AlertModels
{
    public class Alert
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
