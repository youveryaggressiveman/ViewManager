using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
