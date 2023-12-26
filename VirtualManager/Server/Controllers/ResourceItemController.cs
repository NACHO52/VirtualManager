using Microsoft.AspNetCore.Mvc;
using VirtualManager.DAO;
using VirtualManager.Shared;

namespace VirtualManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceItemController : Controller
    {
        private readonly IResourceItemDAO _dao;

        public ResourceItemController(IResourceItemDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<IList<ResourceItem>> ResourceItemGetAll()
        {
            return await _dao.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ResourceItem> ResourceItemGet(int id)
        {
            return await _dao.Get(id);
        }
        [HttpPost]
        public async Task ResourceItemSave(ResourceItem obj)
        {
            await _dao.Save(obj);
        }
        [HttpPut]
        public async Task ResourceItemUpdate(ResourceItem obj)
        {
            await _dao.Update(obj);
        }
        [HttpDelete("{id}")]
        public async Task ResourceItemDelete(int id)
        {
            await _dao.Delete(id);
        }
    }

}