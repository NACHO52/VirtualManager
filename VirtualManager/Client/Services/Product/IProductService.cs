using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public interface IProductService
    {
        Task<IList<Product>> ProductGetAll();
        Task<Product> ProductGet(int id);
        Task<Product> ProductSave(Product obj);
        Task ProductDelete(int id);
    }
}
