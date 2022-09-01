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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorBL collaboratorBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext context;
        private readonly IDistributedCache distributedCache;

        public CollaboratorController(ICollaboratorBL collaboratorBL, IMemoryCache memoryCache, FundooContext context, IDistributedCache distributedCache)
        {
            this.collaboratorBL = collaboratorBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
        }
        [Authorize]
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
        [Authorize]
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

        [Authorize]
        [HttpGet]
        [Route("Read")]
        public IActionResult ReadCollaborate(long colabId)
        {
            try
            {

                //string readnoteId = User.FindFirst(ClaimTypes.Email).Value.ToString();
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = collaboratorBL.ReadCollaborate(colabId, userId);
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

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollaboratorUsingRedisCache()
        {
            var cacheKey = "CollabratorList";
            string serializedcollaboratorList;
            var collaboratorList = new List<CollaboratorEntity>();
            var rediscollaboratorList = await distributedCache.GetAsync(cacheKey);
            if (rediscollaboratorList != null)
            {
                serializedcollaboratorList = Encoding.UTF8.GetString(rediscollaboratorList);
                collaboratorList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedcollaboratorList);
            }
            else
            {
                collaboratorList = await context.CollaboratorTable.ToListAsync();
                serializedcollaboratorList = JsonConvert.SerializeObject(collaboratorList);
                rediscollaboratorList = Encoding.UTF8.GetBytes(serializedcollaboratorList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, rediscollaboratorList, options);
            }
            return Ok(collaboratorList);
        }




    }
}
