using Microsoft.AspNetCore.Mvc;
using VirtualManager.DAO;
using VirtualManager.Shared;

namespace VirtualManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : Controller
    {
        private readonly IResourceDAO _dao;

        public ResourceController(IResourceDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<IList<Resource>> ResourceGetAll()
        {
            return await _dao.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<Resource> ResourceGet(int id)
        {
            return await _dao.Get(id);
        }
        [HttpPost]
        public async Task ResourceSave(Resource obj)
        {
            await _dao.Save(obj);
        }
        [HttpPut]
        public async Task ResourceUpdate(Resource obj)
        {
            await _dao.Update(obj);
        }
        [HttpDelete("{id}")]
        public async Task ResourceDelete(int id)
        {
            await _dao.Delete(id);
        }
    }

}