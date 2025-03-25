using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public enum ERole
    {
        Admin, User
    }
    public class User
    {
        [Key]
        public int Id { get; set; } //מזהה יחודי
        public int UserId { get; set; } // מזהה משתמש
        public string Name { get; set; } // שם משתמש
        public string Email { get; set; } // אימייל ייחודי
        public string PasswordHash { get; set; } // סיסמה מוצפנת
        public ERole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;// תאריך הצטרפות
        public bool IsDeleted { get; set; } = false;
        public List<Creation> UserCreationList { get; set; } = new List<Creation>();

        //public List<int> UserChallengeVotes { get; set; } = new List<int>();//האתגר שהמשתמש הצביע אליו


        public override string ToString()
        {
            return $"User: {Name}, Email: {Email}, Created At: {CreatedAt}";
        }
    }
}
