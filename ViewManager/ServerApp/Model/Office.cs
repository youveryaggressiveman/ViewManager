using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class Office
    {
        public string Id { get; set; } = null!;
        public string Value { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<LogByOffice> LogByOffices { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
