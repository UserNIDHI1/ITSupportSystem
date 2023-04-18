using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class PermissionViewModel : BaseEntity
    {
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public Guid RoleId { get; set; }
        public Guid FormId { get; set; }
        public string FormName { get; set; }
    }
}
