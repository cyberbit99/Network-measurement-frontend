using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Shared.Model
{
    enum MesurementType
    {
        SPT=1, //Speedtest
        WSS=2, //Wifi signal streinght
        DSC=3 //Device scan
    }


    public partial class Measurement
    {
        public int MeasurementId { get; set; }
        public int MeasurementReportId { get; set; }
        public int MeasurementTypeId { get; set; }
        public string MeasuredData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
