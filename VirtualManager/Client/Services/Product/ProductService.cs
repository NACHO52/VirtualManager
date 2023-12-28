using System.Net.Http.Json;
using VirtualManager.Shared;

namespace VirtualManager.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ProductDelete(int id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }

        public async Task<Product> ProductGet(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/product/{id}");
        }

        public async Task<IList<Product>> ProductGetAll()
        {
            return await _httpClient.GetFromJsonAsync<IList<Product>>($"api/product");
        }

        public async Task<Product> ProductSave(Product obj)
        {
            if (obj.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Product>($"api/product", obj);
                obj = await _httpClient.GetFromJsonAsync<Product>($"api/product/GetLast");
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Product>($"api/product", obj);
            }
            return obj;
        }
    }
}
