using GeneralLogic.Model;
using GeneralLogic.Services.Files;
using GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.PcFeatures.ArrangeData
{
    public class ArrangeHelper
    {
        private readonly Visitor _visitor;

        public ArrangeHelper()
        {
            _visitor = new();
        }

        public Data GetArrangeData()
        {
            var data = _visitor.Monitor();

            var dataArray = data.Split("\n");

            Data newData = new();
            newData.NameHardware = new List<string>();

            foreach (var item in dataArray)
            {
                if(double.TryParse(item, out double outResult))
                {
                    newData.Load += outResult;
                }
                else
                {
                    if(item != "\n")
                    {
                        newData.NameHardware.Add(item);
                    }
                }
            }

            newData.Load = Math.Round(newData.Load, 2);

            return newData;
        }
    }
}
