﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase // Interface
    {
        private readonly IUserBL userBL; // Object

        public UserController(IUserBL userBL) // Constructor
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")] // we are giving the route
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


    }
}