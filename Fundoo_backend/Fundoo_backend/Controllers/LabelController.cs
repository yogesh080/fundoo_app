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
        public ActionResult AddLabel(long userId,long notesId, string labelname)
            

        {
            try
            {
                long UserId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = labelBL.AddLabel(userId,notesId, labelname);

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
    }
}