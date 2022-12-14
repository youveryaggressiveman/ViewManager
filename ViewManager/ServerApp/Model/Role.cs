using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
