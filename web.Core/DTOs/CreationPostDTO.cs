using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.DTOs
{
    public class CreationPostDTO
    {
        public int UserId { get; set; } // מזהה המשתמש היוצר
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int ChallengeId { get; set; } // מזהה האתגר אליו שייכת היצירה
        public string ImageUrl { get; set; } // קישור לתמונה
    }
}
