using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public interface IResourceService
    {
        Task<IList<Resource>> ResourceGetAll();
        Task<Resource> ResourceGet(int id);
        Task ResourceSave(Resource obj);
        Task ResourceDelete(int id);
    }
}
