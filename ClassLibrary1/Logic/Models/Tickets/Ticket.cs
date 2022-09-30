using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic.Models.Tickets
{
    public class Ticket
    {
        public Ticket()
        {

        }
        public int ID { get; set; }
        
        public Client client = new Client();
        
        public bool isDeleted { get; set; }
        
        public bool Confirmed { get; set; }

        public string Description { get; set; }
       
        public int SerialNumber { get; set; }
    }
}
