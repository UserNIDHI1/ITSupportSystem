using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class FormViewModel : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string NavigateURL { get; set; }

        public Guid? ParentFormId { get; set; }

        [Required]
        public string FormAccessCode { get; set; }

        [Required]
        public int DisplayIndex { get; set; }

        public string ParentFormName { get; set; }

        public List<DropdownForm> FormDropDown { get; set; }
    }

    public class DropdownForm
    {
        public Guid ParentFormId { get; set; }
        public string ParentFormName { get; set; }
    }

}
