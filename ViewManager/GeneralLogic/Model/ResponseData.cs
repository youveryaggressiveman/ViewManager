using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralLogic.Model
{
    public class ResponseData
    {
        public object? TranslatedText { get; set; }
        public object? Match { get; set; }
        public object? QuotaFinished { get; set; }
        public object? MtLangSupported { get; set; }
        public object? ResponseDetails { get; set; }
        public object? ResponseStatus { get; set; }
        public object? ResponsderId { get; set; }
        public object? Exception_code { get; set; }

        public MatchData[]? Matches { get; set; }
    }
}
