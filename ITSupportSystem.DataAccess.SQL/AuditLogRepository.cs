using ITSupportSystem.Core1.Models;
using ITSupportSystem.Core1.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.DataAccess.SQL
{

    public interface IAuditLogRepository
    {
        IQueryable<AuditLog> Collection();
        void Insert(AuditLog auditLog);
        void commit();
        AuditLog Find(Guid Id);
        List<AuditLogViewModel> GetAuditLogList(bool IsException);
        AuditLogViewModel GetAuditLogById(Guid Id);

    }
    public class AuditLogRepository : IAuditLogRepository
    {
        internal DataContext contex;
        internal DbSet<AuditLog> dbSet;

        public AuditLogRepository()
        {
            contex = new DataContext();
            this.dbSet = contex.Set<AuditLog>();
        }

        public IQueryable<AuditLog> Collection()
        {
            return dbSet;
        }

        public void commit()
        {
            contex.SaveChanges();
        }

        public AuditLog Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(AuditLog auditLog)
        {
            dbSet.Add(auditLog);
        }

        //get the list of the activitylog and errorlog
        public List<AuditLogViewModel> GetAuditLogList(bool IsException)
        {
            var auditLogList = (from a in contex.AuditLog
                                join u in contex.User on a.UserId equals u.Id
                                where (IsException && a.Exception != null) || (!IsException && a.Exception == null)
                                select new AuditLogViewModel()
                                {
                                    Id = a.Id,
                                    UserId = u.Id,
                                    ExecutionTime = a.ExecutionTime,
                                    ExecutionDuration = a.ExecutionDuration,
                                    ClientIpAddress = a.ClientIpAddress,
                                    BrowserInfo = a.BrowserInfo,
                                    HttpMethod = a.HttpMethod,
                                    Url = a.Url,
                                    HttpStatusCode = a.HttpStatusCode,
                                    Comments = a.Comments,
                                    UserName = u.UserName,
                                    Parameters = a.Parameters,
                                    Exception = a.Exception
                                }).ToList();
            return auditLogList;
        }

        //for show the details of activitylog and errorlog
        public AuditLogViewModel GetAuditLogById(Guid Id)
        {
            var getAuditLog = (from a in contex.AuditLog
                               join u in contex.User on a.UserId equals u.Id
                               where a.Id == Id
                               select new AuditLogViewModel()
                               {
                                   Id = a.Id,
                                   UserId = u.Id,
                                   ExecutionTime = a.ExecutionTime,
                                   ExecutionDuration = a.ExecutionDuration,
                                   ClientIpAddress = a.ClientIpAddress,
                                   BrowserInfo = a.BrowserInfo,
                                   HttpMethod = a.HttpMethod,
                                   Url = a.Url,
                                   HttpStatusCode = a.HttpStatusCode,
                                   Comments = a.Comments,
                                   UserName = u.UserName,
                                   Parameters = a.Parameters,
                                  Exception=a.Exception
                               }).FirstOrDefault();
            return getAuditLog;
        }
    }
}
