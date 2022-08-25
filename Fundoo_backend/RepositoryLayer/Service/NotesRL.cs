using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly IConfiguration _AppSetting;

        private readonly FundooContext fundooContext;

        


        public NotesRL(FundooContext fundooContext, IConfiguration _AppSetting)
        {
            this.fundooContext = fundooContext;
            this._AppSetting = _AppSetting;


        }
        public NotesEntity AddNotes(NoteCreateModel notesCreateModel, long userId)
        {
            try
            {

                //var result = fundooContext.NotesTable.FirstOrDefault(e => e.UserId == userId);
                //if (result != null)
                //{


                    NotesEntity notesEntity = new NotesEntity();
                    notesEntity.Title = notesCreateModel.Title;
                    notesEntity.Description = notesCreateModel.Description;
                    notesEntity.Color = notesCreateModel.Color;
                    notesEntity.Remainder = notesCreateModel.Remainder;
                    notesEntity.Image = notesCreateModel.Image;
                    notesEntity.Archive = notesCreateModel.Archive;
                    notesEntity.Pin = notesCreateModel.Pin;
                    notesEntity.Trash = notesCreateModel.Trash;
                    notesEntity.CreateTime = DateTime.Now;
                    notesEntity.ModifiedTime = notesCreateModel.ModifiedTime;
                    notesEntity.UserId = userId;

                    fundooContext.NotesTable.Add(notesEntity);
                    fundooContext.SaveChanges();

                    return notesEntity;
                //}
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception)
            {
                throw;
            }

        }




    }

}
