using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class User
    {
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Email { get; set; } = null;

        public int? StaffId { get; set; } = null;

        public bool Enabled { get; set; } = true;
    }
}
