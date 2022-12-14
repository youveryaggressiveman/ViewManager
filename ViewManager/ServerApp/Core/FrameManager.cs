using ServerApp.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServerApp.Core
{
    public static class FrameManager
    {
        private readonly static ISettingsManager _settingsManager = new SettingsManager();

        public static Frame MainFrame { get; set; } = null;
        public static Frame MainPageFrame { get; set; } = null;

        public static void SetPage<T>(T target, string frameName) where T: Page
        {
            Type content = typeof(Frame);

            if (frameName == "mainFrame")
            {
                content = MainFrame.Content?.GetType();

                if (content != typeof(T))
                {
                    MainFrame.Navigate(target);
                    MainFrame.NavigationService.RemoveBackEntry();

                    GC.Collect();
                }
            }
            else
            {
                content = MainPageFrame.Content?.GetType();

                if (content != typeof(T))
                {
                    _settingsManager.SetTheme(Properties.Settings.Default.ThemeName);

                    MainPageFrame.Navigate(target);
                    MainPageFrame.NavigationService.RemoveBackEntry();

                    GC.Collect();
                }
            }

           
        }
    }
}
