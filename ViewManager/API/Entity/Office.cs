using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class Office
    {
        public Office()
        {
            LogByOffices = new HashSet<LogByOffice>();
            Users = new HashSet<User>();
        }

        public string Id { get; set; } = null!;
        public string Value { get; set; } = null!;

        public virtual ICollection<LogByOffice> LogByOffices { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
