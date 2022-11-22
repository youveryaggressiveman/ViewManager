using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class ConnectedClient
    {
        public string Ip { get; set; } = null;
        public int Port { get; set; }
        public string Name { get; set; } = null;
    }
}
