using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic.Models.Tickets
{
    public class TicketsRequest
    {
        public int ID { get; set; }

        public Client client = new Client();
        
        public Ticket ticket = new Ticket();
        
        public Status status;

        //public int ClientID { get; set; }

        //public int ItemID { get; set; }

        //public int StatusID { get; set; }

    }
}
