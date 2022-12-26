using ServerApp.ViewModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ServerApp.Core.Screen
{
    public class ToScreenConverter : BaseViewModel
    {
        public static BitmapImage? S_Image { get; set; }

        public static void Convert(byte[] bytes)
        {
            using (MemoryStream memoryStream = new(bytes))
            {
                System.Drawing.Bitmap bmp =
                (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
                S_Image = ConvertToBitmapImage(bmp);
            }
        }

        private static BitmapImage ConvertToBitmapImage(Bitmap src)
        {
            BitmapImage image = new BitmapImage();

            using (MemoryStream memoryStream = new())
            {
                ((System.Drawing.Bitmap)src).Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);

                memoryStream.Seek(0, SeekOrigin.Begin);

                image.BeginInit();
                image.StreamSource = memoryStream;
                image.EndInit();

                return image;
            } 
        }
    }
}
