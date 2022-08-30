using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public NoteLabel AddLabel(long userId, long notesId, string labelname);
        public IEnumerable<NoteLabel> ReadLabel(long labelId);
        public string DeleteLabel(long labelId);


        //public IEnumerable<NoteLabel> ReadLabel(long labelId, string labelname);
        //public string DeleteLabel(long notesId, string labelname);





    }
}
