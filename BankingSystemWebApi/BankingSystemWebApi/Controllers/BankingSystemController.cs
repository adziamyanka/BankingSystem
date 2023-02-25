using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BankingSystemWebApi.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingSystemController : ControllerBase
    {
        private static UserManager _userManager = new ();
        private readonly IUserManager userManager2;
        public BankingSystemController(IUserManager userManager)
        {
            userManager2 = userManager;
        }

        [HttpGet("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _userManager.GetAllUsers();
        }

        [HttpGet("GetUser")]
        public User GetUser(string userName)
        {
            return _userManager.GetUserByName(userName.ToLower());
        }

        [HttpPost("CreateUser")]
        public ActionResult CreateUser(string userName)
        {
            try
            {
                var user = _userManager.CreateUser(userName.ToLower());
                return new ObjectResult(user);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }

        [HttpPost("CreateAccount")]
        public ActionResult CreateAccount(string userName, int balance)
        {
            try
            {
                _userManager.CreateAccount(userName.ToLower(), balance);
                return new JsonResult(new { status = ResponseStatus.Ok, message = "Account was created." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }

        [HttpDelete("DeleteAccount")]
        public ActionResult DeleteAccount(string userName, [Required] int accountId)
        {
            try
            {
                _userManager.DeleteAccount(userName.ToLower(), accountId);
                return new JsonResult(new { status = ResponseStatus.Ok, message = "Account was deleted." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser([Required] string userName)
        {
            try
            {
                _userManager.DeleteUser(userName.ToLower());
                return new JsonResult(new { status = ResponseStatus.Ok, message = "Account was deleted." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }

        [HttpPost("DepositToAccount")]
        public ActionResult DepositToAccount(string userName, int accountId, int deposit)
        {
            try
            {
                _userManager.DepositToAccount(userName.ToLower(), accountId, deposit);
                return new JsonResult(new { status = ResponseStatus.Ok, message = "Deposit was successful." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }

        [HttpPost("WithdrawFromAccount")]
        public ActionResult WithdrawFromAccount(string userName, int accountId, int withdrawal)
        {
            try
            {
                _userManager.WithdrawFromAccount(userName.ToLower(), accountId, withdrawal);
                return new JsonResult(new { status = ResponseStatus.Ok, message = "Withdrawal was successful." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = ResponseStatus.Error, message = ex.Message });
            }
        }
    }
}
