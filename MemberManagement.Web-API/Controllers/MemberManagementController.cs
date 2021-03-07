using System.Collections.Generic;
using ManagementSystem.Data;
using ManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemberManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberManagementController : Controller
    {
        private readonly IUserRepository _userRepository;

        public MemberManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUsers()));
        }
        
        [HttpGet]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            return Ok(JsonConvert.SerializeObject(_userRepository.DeleteUser(userId)));
        }

        [HttpGet]
        [Route("AddUser")]
        public ActionResult AddUser(User userDetails)
        {
            return  Ok(JsonConvert.SerializeObject(_userRepository.AddUser(userDetails)));
        }

        [HttpGet]
        [Route("UpdateUser")]
        public User UpdateUser(User userDetails)
        {
            return _userRepository.UpdateUser(userDetails);
        }
        
        [HttpGet]
        [Route("GetUser")]
        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        
    }
}