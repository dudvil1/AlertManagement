using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertManagement.AddAlert.AlertModels.AlertModels
{
    public class AddAlertRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{2,6}$", ErrorMessage = "Flight number format is invalid.")]
        public string FlightNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FlightDate { get; set; }

        public string Note { get; set; }
    }
}
