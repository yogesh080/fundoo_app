using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorBL collaboratorBL;

        public CollaboratorController(ICollaboratorBL collaboratorBL)
        {
            this.collaboratorBL = collaboratorBL;
        }
        [HttpPost]
        [Route("Add")]
        public ActionResult AddCollaborate(long notesId, string Email)
        {
            try
            {
                
                var result = collaboratorBL.AddCollaborate(notesId, Email);
                if(result != null)
                {
                    return Ok(new { success = true, message = "CREATED SUCCESFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CREATATION FAIL" });
                }


            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteCollaborate(long notesId, string Email)
        {

            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = collaboratorBL.DeleteCollaborate(notesId, Email);
            if(result != null)
            {
                return Ok(new { success = true, message = "Collaborater Email deleted", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Collaborater Email not deleted" });
            }


        }


        [HttpGet]
        [Route("Read")]
        public IActionResult ReadCollaborate()
        {
            try
            {
                
                string noteId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = collaboratorBL.ReadCollaborate(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "COLLABRATION EMAIL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "COLLABRATION RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }




    }
}
