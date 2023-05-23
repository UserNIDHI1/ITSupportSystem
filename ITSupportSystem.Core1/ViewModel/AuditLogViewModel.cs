using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.ViewModel
{
    public class AuditLogViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long ExecutionDuration { get; set; }
        public string ClientIpAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public string Exception { get; set; }
        public int HttpStatusCode { get; set; }
        public string Comments { get; set; }
        public string Parameters { get; set; }
        public string Headers { get; set; }
        public string UserName { get; set; }  //for get the usernamne
    }
}
