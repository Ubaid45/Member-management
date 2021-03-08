using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class AccountManagementController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountManagementController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region CRUD
        
        [HttpGet]
        [Route("GetAllAccounts")]
        public ActionResult GetAllAccounts()
        {
            try
            {
                return Ok(ResponseToJson(GetAllAccountDetails()));
            }
            
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Getting the account collection"});
            }
            
        }
        
           
        [HttpGet]
        [Route("GetAccount")]
        public ActionResult GetAccountById(int accountId)
        {
            try
            {
                var accountDetails = _unitOfWork.Accounts.Get(
                    m => m.AccountId == accountId, null, "User").FirstOrDefault();
                return Ok(ResponseToJson(_mapper.Map<AccountDto>(accountDetails)));
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error Getting details"});
            }
                
        }
        
        
        [HttpPost]
        [Route("AddAccount")]
        public ActionResult AddAccount(AccountDto accountDto)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDto);
                _unitOfWork.Accounts.Insert(account);
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="Added account successfully"} 
                    : new { status="error",message="Account is not added"});
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error adding Account"});
            }
                
        }
        
        [HttpPut]
        [Route("UpdateAccount")]
        public ActionResult UpdateAccount(AccountDto accountDto)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDto);
                _unitOfWork.Accounts.Update(account);
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="Account updated successfully"} 
                    : new { status="error",message="Account is not updated"});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error updating Account"});
            }
        }
        
        [HttpDelete]
        [Route("DeleteAccount")]
        public ActionResult DeleteAccount(int accountId)
        {
            try
            {
                _unitOfWork.Accounts.Delete(accountId);
                var savedChanges =_unitOfWork.Commit();

                return Json(savedChanges > 0 ? new { status="success",message="Account deleted successfully"} 
                    : new { status="error",message="Account is not deleted"});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error deleting Account"});
            }
        }
        #endregion

        #region Private Members

        private List<AccountDto> GetAllAccountDetails()
        {
            var accountList = _unitOfWork.Accounts.Get(
                null, q => q.OrderBy(s => s.AccountId), "User");
           return _mapper.Map<List<AccountDto>>(accountList);
        }
        
        private string ResponseToJson<T>(T data) where T: class
        {
                return data != null
                    ? JsonConvert.SerializeObject(data)
                    : JsonConvert.SerializeObject(new object());
        }
        
        #endregion

        #region Points
    
        [HttpPost]
        [Route("CollectPoints")]
        public ActionResult CollectPoints(int accountId, double points)
        {
            try
            {
                var savedChanges = 0;
                if (_unitOfWork.Accounts.CollectPoints(accountId, points))
                    savedChanges = _unitOfWork.Commit();
                
                return Json(savedChanges > 0 ? new { status="success",message="Points collected successfully"} 
                    : new { status="error",message="Points are collected redeemed"});

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error collecting points"});
            }
        }
        
        [HttpPost]
        [Route("RedeemPoints")]
        public ActionResult RedeemPoints(int accountId, double points)
        {
            try
            {
                var savedChanges = 0;
                if (_unitOfWork.Accounts.RedeemPoints(accountId, points))
                    savedChanges = _unitOfWork.Commit();
                
                return Json(savedChanges > 0 ? new { status="success",message="Points redeemed successfully"} 
                    : new { status="error",message="Points are not redeemed"});
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return Json(new { status="error",message="Error redeeming points"});
            }
        }
        

        #endregion
    }
}