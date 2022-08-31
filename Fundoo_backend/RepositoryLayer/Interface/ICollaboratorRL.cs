using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        public CollaboratorEntity AddCollaborate(long notesId, string Email);
        public string DeleteCollaborate(long notesId, string Email);
        //public IEnumerable<CollaboratorEntity> ReadCollaborate(long noteid, long colabId);
        public CollaboratorEntity ReadCollaborate(long colabId, long userId);


    }
}
