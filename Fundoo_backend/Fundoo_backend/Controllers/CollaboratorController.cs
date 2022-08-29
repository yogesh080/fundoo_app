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
        public ActionResult AddCollaborate(long notesId,CollabResponseModel model)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

            if (userId == 0 && notesId == 0)
            {
                return BadRequest(new { Success = false, message = "Email Missing For Collaboration" });
            }

            CollabResponseModel collaborate = collaboratorBL.AddCollaborate(notesId, userId, model);
            return Ok(new { Success = true, message = "Collaboration Successfull ", userId });

        }

    }
}
