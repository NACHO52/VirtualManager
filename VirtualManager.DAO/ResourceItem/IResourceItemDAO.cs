using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public interface IResourceItemDAO
    {
        Task<IList<ResourceItem>> GetAll();
        Task<ResourceItem> Get(int id);
        Task Save(ResourceItem obj);
        Task Update(ResourceItem obj);
        Task Delete(int obj);
    }
}
