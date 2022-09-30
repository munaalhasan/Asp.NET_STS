using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.TicketsRequest
{
    public class RequestRecord
    {
        public int RequestId { get; set; }
        public string RequestFromClientFullName { get; set; }
        public string TicketDescription { get; set; }
        public string status { get; set; }
    }
}
