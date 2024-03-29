﻿using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class TicketViewModel : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AssignTo { get; set; }
        public Guid TypeId { get; set; }
        public Guid PriorityId { get; set; }
        public Guid StatusId { get; set; }

        public string Image { get; set; }

        //for get the all list of dropdown
        public List<DropDown> AssignedDropDown { get; set; }
        public List<DropDown> TypeDropDown { get; set; }
        public List<DropDown> PriorityDropDown { get; set; }
        public List<DropDown> StatusDropDown { get; set; }

        public string Assigned { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        //for attach the image
        public string Attachment { get; set; }
        //for attach the ticket image
        public List<TicketAttachment> TicketAttachment { get; set; }
        //For image attachment count
        public int AttachmentCount { get; set; }

        //for get the createdBy which is linked with User table
        public string CreatedByName { get; set; }
    }
}
