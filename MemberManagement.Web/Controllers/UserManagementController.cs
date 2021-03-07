using System;
using ManagementSystem.Data;
using ManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemberManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUsers()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            try
            {
                if (_userRepository.DeleteUser(userId))
                    return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUsers()));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("AddUser")]
        public ActionResult AddUser(User userDetails)
        {
            try
            {
                if (_userRepository.AddUser(userDetails))
                    return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUsers()));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("UpdateUser")]
        public ActionResult UpdateUser(User userDetails)
        {
            try
            {
                if (_userRepository.UpdateUser(userDetails))
                    return Ok(JsonConvert.SerializeObject(_userRepository.GetAllUsers()));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("GetUser")]
        public ActionResult GetUserById(int userId)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(_userRepository.GetUserById(userId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}