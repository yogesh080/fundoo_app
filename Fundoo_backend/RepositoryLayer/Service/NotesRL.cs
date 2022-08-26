using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly IConfiguration _AppSetting;

        private readonly FundooContext fundooContext;

        private readonly IConfiguration cloudinaryEntity;



        public NotesRL(FundooContext fundooContext, IConfiguration _AppSetting, IConfiguration cloudinaryEntity)
        {
            this.fundooContext = fundooContext;
            this._AppSetting = _AppSetting;
            this.cloudinaryEntity = cloudinaryEntity;


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

        public IEnumerable<NotesEntity> ReadNotes(long userId)      //retrieve from collection
        {
            try
            {
                var result = this.fundooContext.NotesTable.Where(x => x.UserId == userId);
                return result;
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
                var result = fundooContext.NotesTable.Where(note => note.UserId == userId && note.NotesId == NoteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteModel.Title;
                    result.Description = noteModel.Description;
                    result.Color = noteModel.Color;
                    result.Remainder = noteModel.Remainder;
                    result.Image = noteModel.Image;
                    result.ModifiedTime = DateTime.Now;

                    this.fundooContext.SaveChanges();
                    return result;
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

        public bool DeleteNotes(long userId, long noteId)
        {
            try
            {

                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NotesId == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NotesId == noteId).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;

                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NotesId == noteId).FirstOrDefault();

                if (result.Archive == false)
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NotesId == noteId).FirstOrDefault();

                if (result.Trash == false)
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Image(IFormFile image, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userID && x.NotesId == noteID).FirstOrDefault();
                if (result != null)
                {
                    Account cloudaccount = new Account(
                        cloudinaryEntity["CloudinarySettings:cloud_name"],
                        cloudinaryEntity["CloudinarySettings:api_key"],
                        cloudinaryEntity["CloudinarySettings:api_secret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(cloudaccount);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image uploaded successfully";
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





    }

}
