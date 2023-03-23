using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Guid Id { get; set; }


        [Required]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<DropDown> RoleDropDown { get; set; }
    }

    public class DropDown
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
