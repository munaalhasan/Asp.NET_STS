using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Support_Tickets_System.Website.Models.TicketsRequest
{
    public class RequestModel
    {       
        public string message { get; set; }
    }
}
