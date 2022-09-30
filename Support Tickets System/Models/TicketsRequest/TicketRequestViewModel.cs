using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.TicketsRequest
{
    public class TicketRequestViewModel
    {
        public List<RequestRecord> RequestsList { get; set; }

        public TicketRequestViewModel()
        {
            RequestsList = new List<RequestRecord>();
        }
    }
}
