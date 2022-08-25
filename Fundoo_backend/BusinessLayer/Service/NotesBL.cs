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
    }

}
