using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp.Core.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public void SetLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public void SetTheme(string theme)
        {
            Uri uri = new(@"../../../Assets/Styles/Themes/" + theme + ".xaml", UriKind.Relative);

            ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
