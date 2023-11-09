using Microsoft.AspNetCore.Mvc;
using VirtualManager.DAO;
using VirtualManager.Shared;

namespace VirtualManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : Controller
    {
        private readonly ISystemUserDAO _userDAO;

        public SystemUserController(ISystemUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [HttpGet]
        public async Task<IList<SystemUser>> SystemUserGetAll()
        {
            return await _userDAO.GetAll();
        }
    }

}