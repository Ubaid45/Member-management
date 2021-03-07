using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagementSystem.Data;
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
                return Ok(GetAllAccountDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return Ok(JsonConvert.SerializeObject(_mapper.Map<AccountDto>(accountDetails)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                _unitOfWork.Commit();
                
                return Ok(GetAllAccountDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("UpdateAccount")]
        public ActionResult UpdateAccount(AccountDto accountDto)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDto);
                _unitOfWork.Accounts.Update(account);
                _unitOfWork.Commit();
                
                return Ok(GetAllAccountDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        [Route("DeleteAccount")]
        public ActionResult DeleteAccount(int accountId)
        {
            try
            {
                _unitOfWork.Accounts.Delete(accountId);
                _unitOfWork.Commit();
                
                return Ok(GetAllAccountDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetAllAccountDetails()
        {
            var accountList = _unitOfWork.Accounts.Get(
                null, q => q.OrderBy(s => s.AccountId), "User");
           return JsonConvert.SerializeObject(_mapper.Map<List<AccountDto>>(accountList));
        }
        
        #endregion

        #region Points
    
        [HttpPost]
        [Route("CollectPoints")]
        public ActionResult CollectPoints(int accountId, double points)
        {
            try
            {
                if (!_unitOfWork.Accounts.CollectPoints(accountId, points)) return BadRequest();
                _unitOfWork.Commit();
                
                return Ok(GetAllAccountDetails());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("RedeemPoints")]
        public ActionResult RedeemPoints(int accountId, double points)
        {
            try
            {
                if (!_unitOfWork.Accounts.RedeemPoints(accountId, points)) return BadRequest();
                
                _unitOfWork.Commit();
                return Ok(GetAllAccountDetails());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        #endregion
    }
}