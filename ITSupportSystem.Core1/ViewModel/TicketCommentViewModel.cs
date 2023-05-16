using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class TicketCommentViewModel : BaseEntity
    {
        public Guid TicketId { get; set; }
        public string Comment { get; set; }
        public string CreatedByName { get; set; }

        public string Assigned { get; set; }
        public Guid AssignTo { get; set; }
    }
}
