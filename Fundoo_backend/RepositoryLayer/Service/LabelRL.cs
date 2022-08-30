using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public NoteLabel AddLabel(long userId, long notesId, string labelname)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x => x.NotesId == notesId ).FirstOrDefault();
                if (noteResult != null)
                {
                    NoteLabel LabelEntity = new NoteLabel();
                    LabelEntity.NotesId = noteResult.NotesId;
                    LabelEntity.UserId = noteResult.UserId;
                    LabelEntity.LabelName = labelname;
                    fundooContext.Add(LabelEntity);
                    fundooContext.SaveChanges();
                    return LabelEntity;
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


        public string DeleteLabel(long labelId)
        {
            try
            {
                var noteResult = fundooContext.LabelTable.Where(x =>  x.LabelId == labelId).FirstOrDefault();
                if (noteResult != null)

                {
                    fundooContext.LabelTable.Remove(noteResult);
                    this.fundooContext.SaveChanges();
                    return "Delete successfully";
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



        [HttpGet]
        [Route("Get")]
        public IEnumerable<NoteLabel> ReadLabel(long labelId, long noteid)
        {
            try
            {
                var result = this.fundooContext.LabelTable.Where(x => x.LabelId == labelId && x.NotesId == noteid);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
