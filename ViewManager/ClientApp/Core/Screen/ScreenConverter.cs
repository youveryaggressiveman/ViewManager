using System;
using System.Collections.Generic;
using System.Drawing;
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
    }
}
