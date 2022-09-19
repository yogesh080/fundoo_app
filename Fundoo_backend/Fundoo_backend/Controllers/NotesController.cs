using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using RepositoryLayer.Context;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly IMemoryCache memoryCache;
        private readonly FundooContext context;
        private readonly IDistributedCache distributedCache;

        private readonly ILogger<NotesController> _logger;

        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, FundooContext context, IDistributedCache distributedCache, ILogger<NotesController> logger) 
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public ActionResult CreateNote(NoteCreateModel notesCreateModel)
        {

            try
            {
                long userId = Convert.ToInt64(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = notesBL.AddNotes(notesCreateModel, userId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "NOTES CREATED SUCCESFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTES CREATATION FAIL" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }
        //[Authorize]
        //[HttpGet]
        //[Route("Read")]
        //public IActionResult ReadNotes(long noteId)
        //{
        //    try
        //    {
        //        long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
        //        var result = notesBL.ReadNotes(userId, noteId);
        //        if (result != null)
        //        {
        //            return Ok(new { success = true, message = "NOTES RECIEVED", data = result });
        //            throw new Exception("Error Occured");
        //        }
        //        else
        //        {
            
        //            return BadRequest(new { success = false, message = "NOTES RECIEVED FAILED" });
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        [Authorize]
        [HttpGet]
        [Route("Read")]
        public IActionResult ReadAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.ReadAllNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "NOTES RECIEVED", data = result });
                    throw new Exception("Error Occured");
                }
                else
                {

                    return BadRequest(new { success = false, message = "NOTES RECIEVED FAILED" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(NoteCreateModel noteModel, long NoteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.UpdateNote(noteModel, NoteID, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "NOTES UPDATE SUCCESSFULLY", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTES UPDATE FAILED" });
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
        public IActionResult DeleteNotes(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.DeleteNotes(userID, NoteID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "NOTE DELETED" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTE CANNOT DELETE" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNotes(long noteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.PinNotes(userID, noteId);
                if (result != false)
                {
                    return Ok(new { success = true, message = "NOTE PIN" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTE CANNOT PIN" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long noteId)
        {

            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.Archive(noteId, userID);

                if (result == true)
                {
                    return Ok(new { success = true, message = "NOTE ARCHIVE SUCCESSFULL!" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "NOTE ARCHIVE FAIL!" });
                }
                return BadRequest(new { success = false, message = "Operation Fail." });
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long noteId)
        {

            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.Trash(noteId, userID);

                if (result == true)
                {
                    return Ok(new { success = true, message = "NOTE TRANSH SUCCESSFULL!" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "NOTE TRANSH FAIL!" });
                }
                return BadRequest(new { success = false, message = "Operation Fail." });
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Image")]
        public IActionResult Image(IFormFile image, long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = notesBL.Image(image, NoteID, userID);
                if (result != null)
                {

                    return Ok(new { success = true, message = "Image uplaod successfully", data=result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot upload image." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Color")]
        public IActionResult Color(long NoteID, string color)
        {

            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "UserId").Value);
                var result = notesBL.Color(NoteID, color);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Color changed successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Color not changed." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var notesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                notesList = await context.NotesTable.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(notesList);
        }

    }
}
