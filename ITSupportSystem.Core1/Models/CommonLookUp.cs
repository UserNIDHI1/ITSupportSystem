using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class CommonLookUp : BaseEntity
    {
        public string ConfigName { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public int? DisplayOrder { get; set; }
        public string Description { get; set; }
    }
}
