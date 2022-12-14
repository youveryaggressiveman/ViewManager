using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Core.Settings
{
    public interface ISettingsManager
    {
         void SetTheme(string theme);
         void SetLanguage(string language);
    }
}
