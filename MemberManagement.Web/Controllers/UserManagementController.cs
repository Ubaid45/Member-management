using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagementSystem.Data;
using ManagementSystem.Data.DTOs;
using ManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MemberManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("GetAllUsers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                return Ok(GetAllUserDetails());
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
                var userDetails = _unitOfWork.Users.Get(
                    m => m.UserId == userId, null, "Accounts").FirstOrDefault();
                return Ok(JsonConvert.SerializeObject(_mapper.Map<UserDto>(userDetails)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                _unitOfWork.Users.Insert(user);
                _unitOfWork.Commit();
                
                return Ok(GetAllUserDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult UpdateUser(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                _unitOfWork.Users.Update(user);
                _unitOfWork.Commit();
                
                return Ok(GetAllUserDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            try
            {
                _unitOfWork.Users.Delete(userId);
                _unitOfWork.Commit();
                
                return Ok(GetAllUserDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetAllUserDetails()
        {
            var userList = _unitOfWork.Users.Get(
                null, q => q.OrderBy(s => s.UserId), "Accounts");
           return JsonConvert.SerializeObject(_mapper.Map<List<UserDto>>(userList));
        }
    }
}