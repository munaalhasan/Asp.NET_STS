using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.Tickets
{
    public class TicketViewModel
    {
        public List<TicketRecord> TicketsList { get; set; }
        public TicketViewModel()
        {
            TicketsList = new List<TicketRecord>();
        }
    }
}
