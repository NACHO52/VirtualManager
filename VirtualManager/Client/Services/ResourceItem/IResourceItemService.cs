using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public interface IResourceItemService
    {
        Task<IList<ResourceItem>> ResourceItemGetAll();
        Task<ResourceItem> ResourceItemGet(int id);
        Task<ResourceItem> ResourceItemSave(ResourceItem obj);
        Task ResourceItemDelete(int id);
        Task<IList<ResourceItem>> ResourceItemGetExcludedByIds(IList<int> ids);
    }
}
