using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public interface ISystemUserDAO
    {
        Task<IList<SystemUser>> GetAll();
        Task<SystemUser> Get(int id);
        Task Save(SystemUser obj);
        Task Delete(int obj);
    }
}
