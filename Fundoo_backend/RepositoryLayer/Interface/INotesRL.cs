﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NoteCreateModel noteCreateModel, long userId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public NotesEntity UpdateNote(NoteCreateModel noteModel, long NoteId, long userId);
        public bool DeleteNotes(long userId, long noteId);
        public bool PinNotes(long noteId, long userId);
        public bool Archive(long noteId, long userId);
        public bool Trash(long noteId, long userId);
        public NotesEntity Image(IFormFile image, long noteID, long userID);
        public NotesEntity Color(long NoteID, string color);











    }
}
