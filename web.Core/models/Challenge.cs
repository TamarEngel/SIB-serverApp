using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; } // מזהה אתגר
        public string Title { get; set; } // שם האתגר
        public string Description { get; set; } // תיאור האתגר
        public DateTime StartDate { get; set; } = DateTime.UtcNow; // תאריך התחלה - ברירת מחדל להיום
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(7); // תאריך סיום - ברירת מחדל לעוד שבוע
        public int CountCreations { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Creation> ChallengeCreationList { get; set; } = new List<Creation>();

        //[ForeignKey("User")]
        //public int UserId { get; set; } 
        //public User Admin { get; set; }


        public override string ToString()
        {
            return $"Challenge: {Title}, Start: {StartDate}, End: {EndDate} CountCreations:{CountCreations}";
        }
    }
}


