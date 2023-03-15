using ITSupportSystem.Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSupportSystem.Core1.Contracts
{
   public interface IRoleRepository
    {
        IQueryable<Role> Collection();
        void commit();
        void Insert(Role role);
        void Update(Role role);
        Role Find(Guid Id);
        void Delete(Guid Id);
    }
}
