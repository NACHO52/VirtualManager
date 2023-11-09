using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public interface IResourceDAO
    {
        Task<IList<Resource>> GetAll();
        Task<Resource> Get(int id);
        Task Save(Resource obj);
        Task Update(Resource obj);
        Task Delete(int obj);
    }
}
