using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime Remainder { get; set; }
        public string Image { get; set; }
<<<<<<< HEAD
        public string Archive { get; set; }
        public bool Pin { get; set; }
        public DateTime Trash { get; set; }
=======
        public bool Archive { get; set; }
        public bool Pin { get; set; }
        public bool Trash { get; set; }
>>>>>>> createNote
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }
        //user variable will refer to usertable and give userId as foreign key in notesatable 















    }
<<<<<<< HEAD
}
=======
}
>>>>>>> createNote
