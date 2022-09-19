using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNotes(NoteCreateModel noteCreateModel, long userId);
        //public IEnumerable<NotesEntity> ReadNotes(long userId, long noteId);

        public IEnumerable<NotesEntity> ReadAllNotes(long userId);


        public NotesEntity UpdateNote(NoteCreateModel noteModel, long NoteId, long userId);
        public bool DeleteNotes(long userId, long noteId);
        public bool PinNotes(long userId, long noteId);
        public bool Archive(long noteId, long userId);
        public bool Trash(long noteId, long userId);
        public NotesEntity Image(IFormFile image, long noteID, long userID);
        public NotesEntity Color(long NoteID, string color);









    }
}
