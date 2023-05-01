using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Model
{
    public class MatchData
    {
        public object? Id { get; set; }
        public string? Segment { get; set; }
        public string? Translation { get; set; }
        public string? Source { get; set; }
        public string? Target { get; set; }
        public object? Quality { get; set; }
        public object? Reference { get; set; }
        public object? Usage_count { get; set; }
        public object? Subject { get; set; }
        public object? Created_by { get; set; }
        public object? Last_updated_by { get; set; }
        public object? Create_date { get; set; }
        public object? Last_update_date { get; set; }
        public double? Match { get; set; }
    }
}
