using CommonLayer.Model;
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







    }
}
