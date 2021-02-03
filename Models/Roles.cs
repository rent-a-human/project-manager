using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerJohn.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}
