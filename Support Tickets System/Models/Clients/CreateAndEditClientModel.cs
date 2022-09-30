using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Views.Clients
{
    public class CreateAndEditClientModel
    {    
        public int Id { get; set; }                      
        
        [Required]
        public string FullName { get; set; }
                        

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsNewClient { get; set; }

    }
}
