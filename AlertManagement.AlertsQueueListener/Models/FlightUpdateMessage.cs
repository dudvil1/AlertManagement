using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertManagement.AlertsQueueListener.Models
{
    public class FlightUpdateMessage
    {
        public string FlightNumber { get; set; }
        public DateTime FlightDate { get; set; }
        public decimal NewPrice { get; set; } // optional for now
    }
}
