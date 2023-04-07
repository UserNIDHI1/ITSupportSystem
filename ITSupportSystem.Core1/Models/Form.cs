using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class Form : BaseEntity
    {
        public string Name { get; set; }
        public string NavigateURL { get; set; }
        public Guid ParentFormId { get; set; }
        public string FormAccessCode { get; set; }
        public int DisplayIndex { get; set; }
    }
}
