using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Service;
using RepositoryLayer.Entity;
using RepositoryLayer.Context;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult AddLabel(long notesId, string labelname)
            

        {
            try
            {
                long UserId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = labelBL.AddLabel(UserId,notesId, labelname);

                if (result != null)
                {
                    return Ok(new { success = true, message = "LABELED SUCCESFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABELED FAIL" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }

        

        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteLabel(long labelId)
        {

            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var result = labelBL.DeleteLabel(labelId);
            if (result != null)
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
        public IActionResult ReadLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ReadLabel(userId, labelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}