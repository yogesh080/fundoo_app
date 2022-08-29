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
    public class CollaboratorBL : ICollaboratorBL
    {


        public ICollaboratorRL collaboratorRL;

        public CollaboratorBL(ICollaboratorRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }

        public CollabResponseModel AddCollaborate(long notesId, long userId, CollaboratedModel model)
        {

            try
            {
                return collaboratorRL.AddCollaborate(notesId, userId, model);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
