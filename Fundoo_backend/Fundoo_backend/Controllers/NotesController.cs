﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;



namespace Fundoo_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;

        public NotesController(INotesBL notesBL) 
        {
            this.notesBL = notesBL;
        }
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

        [HttpGet]
        [Route("Read")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = notesBL.ReadNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "NOTES RECIEVED", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "NOTES RECIEVED FAILED" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

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
    }
}
