using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Creation
    {
        [Key]
        public int Id { get; set; } // מזהה יצירה
        public string FileName { get; set; } 
        public string FileType { get; set; } 
        public int UserId { get; set; } // מזהה המשתמש היוצר
        public int ChallengeId { get; set; } // מזהה האתגר אליו שייכת היצירה
        public string ImageUrl { get; set; } // קישור לתמונה
        public int Votes { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return $"Submission by User ID: {UserId}, Challenge ID: {ChallengeId}, Created At: {CreatedAt} Votes: {Votes}";
        }
    }
}
