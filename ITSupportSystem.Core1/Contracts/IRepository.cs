using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Contracts
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void save();
        void Delete(Guid Id);
        T Find(Guid Id);
        void Insert(T t);
        void Update(T t);
        
    }
}
