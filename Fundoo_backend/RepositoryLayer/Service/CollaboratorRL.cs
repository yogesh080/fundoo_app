using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using CommonLayer.Model;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {

        private readonly FundooContext fundooContext;

        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }


        public CollaboratorEntity AddCollaborate(long notesId, long userId, CollabResponseModel model)
        {
            try
            {
                
                    CollaboratorEntity collaborateEntity = new CollaboratorEntity();

                    collaborateEntity.CollaboratorID = model.CollaboratorID;
                    collaborateEntity.NotesId = model.NotesId;
                    collaborateEntity.UserId = model.UserId;
                    collaborateEntity.CollaboratedEmail = model.CollaboratedEmail;
                    collaborateEntity.UserId = userId;

                    fundooContext.CollaboratorTable.Add(collaborateEntity);
                    fundooContext.SaveChanges();

                    return collaborateEntity;
                


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
