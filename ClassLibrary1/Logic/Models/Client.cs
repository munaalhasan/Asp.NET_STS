using System;
using System.Collections.Generic;
using System.Text;

namespace SupportTicketsSystem.Services.Logic.Models
{
    public class Client
    {
        public Client()
        {

        }
        public int ID;
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
