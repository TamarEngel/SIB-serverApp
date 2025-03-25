using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.DTOs
{
    public class ChallengePostDTO
    {
        //public int Id { get; set; } //מזהה יחודי
        public string Title { get; set; } // שם האתגר
        public string Description { get; set; } // תיאור האתגר
        public DateTime StartDate { get; set; } // תאריך התחלה
        public DateTime EndDate { get; set; } // תאריך סיום
    }
}
