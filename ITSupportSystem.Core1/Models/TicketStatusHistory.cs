using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class TicketStatusHistory : BaseEntity
    {
        public Guid TicketId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
    }
}
