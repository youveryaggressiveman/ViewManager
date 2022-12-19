using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLogic.Services.PcFeatures.LibreHardwareMonitorLib
{
    public class Visitor
    {
        public string Monitor()
        {
            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            var text = string.Empty;

            foreach (IHardware hardware in computer.Hardware)
            {
                text += ("Hardware: {0}", hardware.Name) + "\n";

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    text += ("\tSubhardware: {0}", subhardware.Name) + "\n";

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        text += ("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value) + "\n";
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    text += ("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value) + "\n";
                }
            }

            computer.Close();

            return text;
        }
    }
}
