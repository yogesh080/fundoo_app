using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        //public CollabResponseModel AddCollaborate(long notesId, long UserId, CollaborationModel model);
        public CollaboratorEntity AddCollaborate(long notesId, long userId, CollabResponseModel model);


    }
}
