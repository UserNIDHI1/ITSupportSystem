using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class TicketComment : BaseEntity
    {
        public string TicketId { get; set; }
        public string Comment { get; set; }
    }
}
