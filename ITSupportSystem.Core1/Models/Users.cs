using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class Users : BaseEntity
    {
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
