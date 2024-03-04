using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Shared.Model
{
    public class NetworkData
    {
        public int StausId { get; set; }
        public string Ssid { get; set; }
        public int IpAddress { get; set; }
        public string GatewayAddress { get; set; }
        public object NativeObject { get; set; }
        public object Bssid { get; set; }
        public object SignalStrength { get; set; }
    }
}
