using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class StatusByLog
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<LogByOffice> LogByOffices { get; set; }
    }
}
