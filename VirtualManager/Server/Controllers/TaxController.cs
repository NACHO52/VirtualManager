using Microsoft.AspNetCore.Mvc;
using VirtualManager.DAO;
using VirtualManager.Shared;

namespace VirtualManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : Controller
    {
        private readonly ITaxDAO _dao;

        public TaxController(ITaxDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<IList<Tax>> TaxGetAll()
        {
            return await _dao.GetAll();
        }

        [HttpGet]
        [Route("GetLast")]
        public async Task<Tax> TaxGetLast()
        {
            return await _dao.GetLast();
        }

        [HttpGet("{id}")]
        public async Task<Tax> TaxGet(int id)
        {
            return await _dao.Get(id);
        }
        [HttpPost]
        public async Task TaxSave(Tax obj)
        {
            await _dao.Save(obj);
        }
        [HttpPut]
        public async Task TaxUpdate(Tax obj)
        {
            await _dao.Update(obj);
        }
        [HttpDelete("{id}")]
        public async Task TaxDelete(int id)
        {
            await _dao.Delete(id);
        }
    }

}