using HidSharp.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace ClientApp.Core.Screen
{
    public class ScreenConverter
    {
        private int _screenWidth;
        private int _screenHeight;
        private Bitmap _backGround;
        Graphics _graphics;

        public ScreenConverter(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _backGround = new Bitmap(_screenWidth, _screenHeight);
            _graphics = Graphics.FromImage(_backGround);
        }

        public byte[] Convert()
        {
            _graphics.CopyFromScreen(0, 0, 0, 0, new Size(_screenWidth, _screenHeight));

            using (MemoryStream memoryStream = new())
            {
                _backGround.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                return memoryStream.ToArray();
            }           
        }

        public List<byte[]> CutMsg(byte[] bt)
        {
            int Lenght = bt.Length;
            byte[] temp;
            List<byte[]> msg = new List<byte[]>();

            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(
                      BitConverter.GetBytes((short)((Lenght / 65500) + 1)), 0, 2);
            memoryStream.Write(bt, 0, bt.Length);

            memoryStream.Position = 0;

            while (Lenght > 0)
            {
                temp = new byte[65500];
                memoryStream.Read(temp, 0, 65500);
                msg.Add(temp);
                Lenght -= 65500;
            }

            return msg;
        }

        public List<byte[]> Test(byte[] bt)
        {
            List<byte[]> msg = new List<byte[]>();
            int count = (int)(bt.Length / 65500);

            if (bt.Length / 65500.0 > count)
                count++;
            else if (count <= 0)
                count++;

            byte[] intBytes = BitConverter.GetBytes(count);
            Array.Reverse(intBytes);
            msg.Add(intBytes);

            using (MemoryStream memoryStream = new(bt))
            {
                System.Drawing.Bitmap bmp =
                (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(memoryStream);

                int currentWidth = bmp.Width / count;
                int currentHeight = bmp.Height;

                int lastWidth = 0;
                for (int i = 0; i < count; i++)
                {
                    var resultBitmap = bmp.Clone(new Rectangle(lastWidth, 0, currentWidth - lastWidth, currentHeight), bmp.PixelFormat);

                    var stream = new MemoryStream();
                    resultBitmap.Save(stream, ImageFormat.Bmp);

                    lastWidth = currentWidth;

                    currentWidth += bmp.Width / count;

                    msg.Add(stream.ToArray());
                }
            }

            return msg;
        }

            
    }
}
