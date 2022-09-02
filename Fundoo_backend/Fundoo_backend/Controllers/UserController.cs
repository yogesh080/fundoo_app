using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Service;
using System;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace Fundoo_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase // Interface
    {
        private readonly IUserBL userBL; // Object
        private readonly ILogger<UserController> _logger;


        public UserController(IUserBL userBL, ILogger<UserController> logger) // Constructor
        {
            this.userBL = userBL;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Registration(UserRegistrationModel userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if(result != null)
                {
                    return Ok(new {success = true, message = "Registration successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration false" });

                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                var result = userBL.Login(userLogin);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login successfull", data = result });
                    throw new Exception("Error Occured");

                }
                else
                {
                    return BadRequest(new { success = false, message = "Login fail" });

                }
                 
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());

                throw;
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public ActionResult ForgetPassword (string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "REST LINK SEND SUCCESSFULL ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "REST LINK SEND FAILED" });

                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetLink")]
        public ActionResult ResetLink(string password, string confirmPassword)
        {

            try
            {

                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    return Ok(new { success = true, message = "REST LINK SEND SUCCESSFULL" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "REST LINK SEND FAILED" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }

        }





    }
}