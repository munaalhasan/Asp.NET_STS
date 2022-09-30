using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Views.Tickets
{
    public class CreateAndEditTicketModel
    {             
        public int ID { get; set; }
        
        public int ClientID { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public int SerialNumber { get; set; }
        
        public bool IsNewTicket { get; set; }
    }
}
