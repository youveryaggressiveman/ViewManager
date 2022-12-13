using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Entity
{
    public partial class Specialization
    {
        public Specialization()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
