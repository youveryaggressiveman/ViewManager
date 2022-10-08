using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class User
    {
        public User()
        {
            Specializations = new HashSet<Specialization>();
        }

        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string OfficeId { get; set; } = null!;

        public virtual Office Office { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
