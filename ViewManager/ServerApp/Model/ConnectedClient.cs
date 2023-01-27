using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ServerApp.Model
{
    public class ConnectedClient
    {
        private string _status;

        public string Ip { get; set; } = null;
        public int Port { get; set; }
        public string Name { get; set; } = null;
        public string Status 
        { 
            get => _status;
            set
            {
                _status = value;

                if (_status == "Connected")
                {
                    Foreground = "#A8E4A0";
                }
                else if (_status == "Disconnected")
                {
                    Foreground = "#EE204D";
                }
            } 
        }
        public string Foreground { get; set; } = null!;
        public BitmapImage Image { get; set; } = null!;
    }
}
