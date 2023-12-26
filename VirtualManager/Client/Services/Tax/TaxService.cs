using System.Net.Http.Json;
using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public class TaxService : ITaxService
    {
        private readonly HttpClient _httpClient;
        public TaxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task TaxDelete(int id)
        {
            await _httpClient.DeleteAsync($"api/tax/{id}");
        }

        public async Task<Tax> TaxGet(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tax>($"api/tax/{id}");
        }

        public async Task<IList<Tax>> TaxGetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<Tax>>($"api/tax");
        }

        public async Task<Tax> TaxSave(Tax obj)
        {
            if (obj.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Tax>($"api/tax", obj);
                obj = await _httpClient.GetFromJsonAsync<Tax>($"api/tax/GetLast");
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Tax>($"api/tax", obj);
            }
            return obj;
        }
    }
}
