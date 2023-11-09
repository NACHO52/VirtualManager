using System.Net.Http.Json;
using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public class SystemUserService : ISystemUserService
    {
        private readonly HttpClient _httpClient;
        public SystemUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IList<SystemUser>> GetAll()
        {
            return (IList<SystemUser>)await _httpClient.GetFromJsonAsync<IList<SystemUser>>($"api/SystemUser");
        }
    }
}
