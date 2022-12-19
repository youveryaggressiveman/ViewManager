using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServerApp.Model
{
    public class Statistics
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public BitmapImage Image { get; set; } = null!;
    }
}
