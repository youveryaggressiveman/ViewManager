using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.Settings
{
    public class CheckSettings
    {
        public string CheckCulture(string value)
        {
            var culture = "";

            switch (value)
            {
                case "English":
                    culture = "en-US";
                    break;
                case "Русский":
                    culture = "ru";
                    break;
                default:
                    break;
            }

            return culture;
        }

        public string CheckLang(string value)
        {
            var lang = "";

            switch (value)
            {
                case "en-US":
                    lang = "English";
                    break;
                case "ru":
                    lang = "Русский";
                    break;
                default:
                    break;
            }

            return lang;
        }
    }
}
