using System;
using System.Collections.Generic;

namespace API.Entity
{
    public partial class LogByOffice
    {
        public int Id { get; set; }
        public string OfficeId { get; set; } = null!;
        public int LogId { get; set; }
        public int StatusByLogId { get; set; }
        public DateTime Time { get; set; }

        public virtual Log Log { get; set; } = null!;
        public virtual Office Office { get; set; } = null!;
        public virtual StatusByLog StatusByLog { get; set; } = null!;
    }
}
