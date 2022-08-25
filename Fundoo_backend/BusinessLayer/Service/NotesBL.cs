using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public NotesEntity AddNotes(NoteCreateModel notesCreateModel, long userId)
        {
            try
            {
                return notesRL.AddNotes(notesCreateModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                return notesRL.ReadNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UpdateNote(NoteCreateModel noteModel, long NoteId, long userId)
        {
            try
            {
                return notesRL.UpdateNote(noteModel, NoteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotes(long userId, long noteId)
        {
            try
            {
                return notesRL.DeleteNotes(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PinNotes(long noteId, long userId)
        {
            try
            {
                return notesRL.PinNotes(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Archive(long noteId, long userId)
        {
            try
            {
                return notesRL.Archive(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trash(long noteId, long userId)
        {
            try
            {
                return notesRL.Trash(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }

}
