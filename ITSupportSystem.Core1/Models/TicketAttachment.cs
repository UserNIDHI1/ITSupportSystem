using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Models
{
    public class TicketAttachment : BaseEntity
    {
        public Guid TicketId { get; set; }
        public string FileName { get; set; }
    }
}
