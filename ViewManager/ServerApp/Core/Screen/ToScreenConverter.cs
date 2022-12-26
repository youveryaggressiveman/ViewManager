using ServerApp.ViewModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ServerApp.Core.Screen
{
    public class ToScreenConverter : BaseViewModel
    {
        public static List<Bitmap?> S_Image { get; set; } = new List<Bitmap?>();

        public static void Convert(byte[] bytes)
        {
            using (MemoryStream memoryStream = new(bytes))
            {
                System.Drawing.Bitmap bmp =
                (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);
                S_Image.Add(bmp);
            }
        }

        public static Bitmap? Draw(Bitmap bitmaping, int width, int height)
        {

            int num = 0;

            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    System.Drawing.Image tmp = bitmaping;
                    g.DrawImageUnscaled(tmp, width * num, 0);
                    num++;
            }

            return bitmap;
        }

        public static void Builder()
        {
            var result = Draw(S_Image[S_Image.Count - 1], S_Image[0].Width, S_Image[0].Height);

            result.Save($@"C:\Users\повелитель\Desktop\New folder (2)\{new Random().Next()}Aboba.png", ImageFormat.Png);

            S_Image = new List<Bitmap?>();
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
