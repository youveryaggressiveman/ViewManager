using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class Log
    {
        public Log()
        {
            LogByOffices = new HashSet<LogByOffice>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<LogByOffice> LogByOffices { get; set; }
    }
}
