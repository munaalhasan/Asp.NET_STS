﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupportTicketsSystem.Website.Models.Roles
{
    public class CreateRole 
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
