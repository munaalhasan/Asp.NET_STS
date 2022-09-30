using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.Tickets
{
    public class TicketRecord
    {
        public int TicketId { get; set; }
        public string TicketDesc { get; set; }
        public int TicketSerialNumber { get; set; }
        public string AssignedToClient { get; set; }
        public string AssignedToClientEmail { get; set; }
        public bool ConfirmedReq { get; set; }
    }
}
