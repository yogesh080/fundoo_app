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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext context;
        private readonly IDistributedCache distributedCache;

        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, FundooContext context, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
        }
        [Authorize]
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


        [Authorize]
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

        [Authorize]
        [HttpGet]
        [Route("Read")]
        public IActionResult ReadLabel(long labelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.ReadLabel(labelId, UserId);
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

        
        

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel( long labelid, string labelname)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = labelBL.UpdateLabel( labelid, labelname);
                if (result != null)
                {
                    return Ok(new { success = true, message = "LABEL UPDATE SUCCESSFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "LABEL UPDATE FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()           
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<NoteLabel>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<NoteLabel>>(serializedLabelList);
            }
            else
            {
                LabelList = await context.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}