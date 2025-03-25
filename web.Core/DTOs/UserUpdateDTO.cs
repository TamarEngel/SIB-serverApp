using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.DTOs
{
    public class UserUpdateDTO
    {
        public string Name { get; set; } // שם משתמש
        public string Email { get; set; } // אימייל ייחודי
    }
}
