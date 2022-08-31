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
using Microsoft.AspNetCore.Mvc;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {

        private readonly FundooContext fundooContext;

        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }


        public CollaboratorEntity AddCollaborate(long notesId, string Email)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x => x.NotesId == notesId).FirstOrDefault();
                var emailResult = fundooContext.UserTable.Where(x => x.Email == Email).FirstOrDefault();
                if (noteResult != null && emailResult != null)
                {
                    CollaboratorEntity collabEntity = new CollaboratorEntity();
                    collabEntity.NotesId = noteResult.NotesId;
                    collabEntity.CollaboratedEmail = emailResult.Email;
                    collabEntity.UserId = emailResult.UserId;
                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
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
                var noteResult = fundooContext.CollaboratorTable.Where(x => x.NotesId == notesId && x.CollaboratedEmail == Email).FirstOrDefault();
                if (noteResult != null)

                {
                    fundooContext.CollaboratorTable.Remove(noteResult);
                    this.fundooContext.SaveChanges();
                    return "Delete successfully" ;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception )
            {
                throw;
            }
        }
        
        public IEnumerable<CollaboratorEntity> ReadCollaborate(string Email)
        {
            try
            {
                var result = this.fundooContext.CollaboratorTable.Where( x => x.CollaboratedEmail == Email);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
