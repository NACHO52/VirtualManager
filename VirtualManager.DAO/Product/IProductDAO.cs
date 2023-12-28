using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualManager.Shared;

namespace VirtualManager.DAO
{
    public interface IProductDAO
    {
        Task<IList<Product>> GetAll();
        Task<Product> Get(int id);
        Task Save(Product obj);
        Task Update(Product obj);
        Task Delete(int obj);
        Task<Product> GetLast();
    }
}
