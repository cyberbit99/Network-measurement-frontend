using Android.Content;
using Android.Net.Wifi;

namespace Network_measurement_frontend.Pages.Android
{
    public class AndroidWSS
    {
        public List<int> GetWSS()
        {
            List<int> rssis = new List<int>();
            var wifiMgr = (WifiManager)MauiApplication.Context.GetSystemService(Context.WifiService);

            var wifiList = wifiMgr.ScanResults;
            foreach (var item in wifiList)
            {
                var wifiLevel = WifiManager.CalculateSignalLevel(item.Level, 100);
                rssis.Add(wifiLevel);
                //Console.WriteLine($"Wifi SSID: {item.Ssid} - Strengh: {wifiLevel}");
            }

            return rssis;
        }
    }
}
