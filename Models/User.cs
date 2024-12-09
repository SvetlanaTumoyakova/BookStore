using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
