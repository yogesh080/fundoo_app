using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public NoteLabel AddLabel(long userId, long notesId, string labelname);



    }
}
