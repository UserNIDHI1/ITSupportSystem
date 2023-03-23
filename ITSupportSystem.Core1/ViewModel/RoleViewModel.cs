using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITSupportSystem.Core1.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public Guid Id { get; set; }
    }
}
