using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServerApp.Model
{
    public class LocalIpModel
    {
        private List<string> _valueList;

        public string Name { get; set; } = null!;
        public List<UnicastIPAddressInformation> IpAddresses { get; set; }

        public List<string> ValueList
        {
            get
            {
                foreach (var ip in IpAddresses)
                {
                    _valueList.Add(Name + ": " + ip.Address.ToString());
                }

                return _valueList;
            }
            set 
            {
                _valueList = value;
            }
        }

        public LocalIpModel()
        {
            ValueList = new List<string>();
        }
    }
}
