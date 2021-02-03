using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagerJohn.Models
{
    public class Users
    {
        //public Users()
       // {
           // Projects = new HashSet<Projects>();
       // }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string userName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<Roles> Roles { get; set; }
        public DateTimeOffset Created { get; set; }
        public Boolean IsActive { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}
