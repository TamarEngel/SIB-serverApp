using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // קשר למשתמש שהצביע

        public int CreationId { get; set; }
        public Creation Creation { get; set; } // קשר ליצירה שהצביעו עליה

        public DateTime VotedAt { get; set; } = DateTime.UtcNow; // תאריך הצבעה
    }
}
