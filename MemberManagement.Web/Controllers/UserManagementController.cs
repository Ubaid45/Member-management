using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using ManagementSystem.Data.DTOs;
using ManagementSystem.Data.Interfaces;
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

        #region CRUD
        
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

        #endregion

        #region Data Import/Export

        
        [HttpGet]
        [Route("ImportDataFromFile")]
        public ActionResult ImportDataFromFile()
        {
            try
            {
                var userCollection = PopulateMembersFromFile();
                foreach (var user in userCollection.Where(user => !_unitOfWork.Users.Get(m => m.UserName == user.UserName).Any()))
                {
                    _unitOfWork.Users.Insert(user);
                }
                
                _unitOfWork.Commit();
                return Ok(GetAllUserDetails());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpGet]
        [Route("ExportUsers")]
        public ActionResult ExportUsers()
        {
            try
            {
                var outputFilePath = SetOutputFilePath();
                
                var userCollection = _unitOfWork.Users.GetFilteredDataToExport();
                
                WriteOutputFile(outputFilePath, userCollection);

                return Ok(GetAllUserDetails());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        #endregion
        

        #region Private members
        private string GetAllUserDetails()
        {
            var userList = _unitOfWork.Users.Get(
                null, q => q.OrderBy(s => s.UserId), "Accounts");
            return JsonConvert.SerializeObject(_mapper.Map<List<UserDto>>(userList));
        }
        
        private  List<User> PopulateMembersFromFile()
        {
            var directory = System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory());
            using var r = new StreamReader(Path.Combine(directory ?? string.Empty,"members.json"));
            var json = r.ReadToEnd();
            var response = JsonConvert.DeserializeObject<List<User>>(json);
            return response;
        }

        private void WriteOutputFile(string outputFilePath, List<ExportUserDto> userCollection)
        {
            //open file stream
            using (var file = System.IO.File.CreateText(outputFilePath))
            {
                var serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, _mapper.Map<List<ExportUserDto>>(userCollection));
            }
        }

        private static string SetOutputFilePath()
        {
            var directory = System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var outputFilePath = Path.Combine(directory ?? string.Empty, "FileOut.json");
            return outputFilePath;
        }

        #endregion
    }
}