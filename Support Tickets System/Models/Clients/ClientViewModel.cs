using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.Clients
{
    public class ClientViewModel
    {
        public List<ClientRecord> ClientsList { get; set; }

        public string ClientsCount { get; set; }
        public ClientViewModel()
        {
            ClientsList = new List<ClientRecord>();
        }
    }
}
