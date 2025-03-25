using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;

namespace web.Core.DTOs
{
    public class UserPostDTO
    { 
        public int UserId { get; set; } // מזהה משתמש
        public string Name { get; set; } // שם משתמש
        public string Email { get; set; } // אימייל ייחודי
        public string PasswordHash { get; set; } // סיסמה מוצפנת
    }
}
