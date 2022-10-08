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
        public static Frame MainFrame { get; set; } = null;

        public static void SetPage<T>(T target) where T: Page
        {
            var content = MainFrame.Content?.GetType();

            if(content != typeof(T))
            {
                MainFrame.Navigate(target);
                MainFrame.NavigationService.RemoveBackEntry();

                GC.Collect();
            }
        }
    }
}
