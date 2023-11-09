using System.Net.Http.Json;
using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient _httpClient;
        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ResourceDelete(int id)
        {
            await _httpClient.DeleteAsync($"api/resource/{id}");
        }

        public async Task<Resource> ResourceGet(int id)
        {
            return await _httpClient.GetFromJsonAsync<Resource>($"api/resource/{id}");
        }

        public async Task<IList<Resource>> ResourceGetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<Resource>>($"api/resource");
        }

        public async Task ResourceSave(Resource obj)
        {
            if (obj.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Resource>($"api/resource", obj);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Resource>($"api/resource", obj);
            }
        }
    }
}
