using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity AddCollaborate(long notesId, string Email);
        public string DeleteCollaborate(long notesId, string Email);

    }
}
