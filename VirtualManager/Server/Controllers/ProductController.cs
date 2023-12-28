using Microsoft.AspNetCore.Mvc;
using VirtualManager.DAO;
using VirtualManager.Shared;

namespace VirtualManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductDAO _dao;

        public ProductController(IProductDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<IList<Product>> ProductGetAll()
        {
            return await _dao.GetAll();
        }

        [HttpGet]
        [Route("GetLast")]
        public async Task<Product> ProductGetLast()
        {
            return await _dao.GetLast();
        }

        [HttpGet("{id}")]
        public async Task<Product> ProductGet(int id)
        {
            return await _dao.Get(id);
        }
        [HttpPost]
        public async Task ProductSave(Product obj)
        {
            await _dao.Save(obj);
        }
        [HttpPut]
        public async Task ProductUpdate(Product obj)
        {
            await _dao.Update(obj);
        }
        [HttpDelete("{id}")]
        public async Task ProductDelete(int id)
        {
            await _dao.Delete(id);
        }
    }

}