using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Users
{
    public class RegisterUser : User
    {
        public string? RepeatPassword { get; set; }
        public RegisterUser() : base() { }
    }
}
