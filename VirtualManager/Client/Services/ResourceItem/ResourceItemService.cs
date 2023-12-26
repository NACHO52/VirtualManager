using System.Net.Http.Json;
using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public class ResourceItemService : IResourceItemService
    {
        private readonly HttpClient _httpClient;
        public ResourceItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ResourceItemDelete(int id)
        {
            await _httpClient.DeleteAsync($"api/resourceItem/{id}");
        }

        public async Task<ResourceItem> ResourceItemGet(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResourceItem>($"api/resourceItem/{id}");
        }

        public async Task<IList<ResourceItem>> ResourceItemGetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<ResourceItem>>($"api/resourceItem");
        }

        public async Task<ResourceItem> ResourceItemSave(ResourceItem obj)
        {
            if (obj.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<ResourceItem>($"api/resourceItem", obj);
                obj = await _httpClient.GetFromJsonAsync<ResourceItem>($"api/resourceItem/GetLast");
            }
            else
            {
                await _httpClient.PutAsJsonAsync<ResourceItem>($"api/resourceItem", obj);
            }
            return obj;
        }
    }
}
