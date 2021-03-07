using ManagementSystem.Data;
using MemberManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemberManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberManagementController : Controller
    {
        private readonly IUserService _userService;

        public MemberManagementController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public void GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            //return View(games);
        }

        
    }
}