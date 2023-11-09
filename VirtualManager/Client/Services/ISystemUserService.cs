using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public interface ISystemUserService
    {
        Task<IList<SystemUser>> GetAll();
    }
}
