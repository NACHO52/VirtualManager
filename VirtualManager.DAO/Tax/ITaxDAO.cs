using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public interface ITaxDAO
    {
        Task<IList<Tax>> GetAll();
        Task<Tax> Get(int id);
        Task Save(Tax obj);
        Task Update(Tax obj);
        Task Delete(int obj);
        Task<Tax> GetLast();
    }
}
