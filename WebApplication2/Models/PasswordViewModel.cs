using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class PasswordViewModel
    {
        public string oldPassword { get; set; }

        public string newPassword { get; set; }
        public User person;
    }
}
