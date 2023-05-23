using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ITSupportSystem.Core1.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The mobile number must be 10 digits long.")]
        public string MobileNo { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 1 lowercase letter, 1 uppercase letter, 1 numeric character, and 1 special character.")]
        public string Password { get; set; }
        

        //for encrypt password
        public string PasswordSalt { get; set; }

        public string CreatedByName { get; set; }

        public Guid Id { get; set; }


        [Required]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public List<DropDown> RoleDropDown { get; set; }
        public UserViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
