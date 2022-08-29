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

        public CollaboratorEntity AddCollaborate(long notesId, string Email)


        {

            try
            {
                return collaboratorRL.AddCollaborate(notesId,Email);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string DeleteCollaborate(long notesId, string Email)
        {
            try
            {

                return collaboratorRL.DeleteCollaborate(notesId,Email);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
