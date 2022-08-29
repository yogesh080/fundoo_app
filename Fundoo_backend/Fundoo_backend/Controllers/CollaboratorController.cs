using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Service;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;

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




    }
}
