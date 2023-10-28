using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Shared
{
    public class Device
    {
        public Device() { }

        public Device(string name, string iPAddress)
        {
            Name = name;
            IPAddress = iPAddress;
        }

        public string Name { get; set; }
        public string IPAddress { get; set; }
    }
}
