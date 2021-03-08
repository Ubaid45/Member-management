using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return Ok(ResponseToJson(GetAllUserDetails()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Getting the user collection"});
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
                return Ok(ResponseToJson(_mapper.Map<UserDto>(userDetails)));
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Getting user details"});
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
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="Added user successfully"} 
                    : new { status="error",message="User is not added"});
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error adding User"});
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
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="User updated successfully"} 
                    : new { status="error",message="User is not updated"});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error updating User"});
            }
        }
        
        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(int userId)
        {
            try
            {
                _unitOfWork.Users.Delete(userId);
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="User deleted successfully"} 
                    : new { status="error",message="User is not deleted"});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error deleting User"});
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
                
                var savedChanges = _unitOfWork.Commit();
                
                return Json(savedChanges > 0 ? new { status="success",message="Data is successfully imported"} 
                    : new { status="error",message="Some error happened while importing"});

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Importing data"});
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

                return Json(new {status = "success", message = "Data is successfully exported"});
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Exporting data"});
            }
            
        }
        
        #endregion
        

        #region Private members
        
        private string ResponseToJson<T>(T data) where T: class
        {
            return data != null
                ? JsonConvert.SerializeObject(data)
                : JsonConvert.SerializeObject(new object());
        }
        
        private List<UserDto> GetAllUserDetails()
        {
            var userList = _unitOfWork.Users.Get(
                null, q => q.OrderBy(s => s.UserId), "Accounts");
            return _mapper.Map<List<UserDto>>(userList);
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