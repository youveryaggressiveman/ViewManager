using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class StatusByLog
    {
        public StatusByLog()
        {
            LogByOffices = new HashSet<LogByOffice>();
        }

        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<LogByOffice> LogByOffices { get; set; }
    }
}
