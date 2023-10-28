using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_frontend.Measurements
{
    public class SpeedTest
    {
        public async Task<double> DownloadSpeedTest(string downloadUrl)
        {
            var httpClient = new HttpClient();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            HttpResponseMessage response = await httpClient.GetAsync(downloadUrl);
            stopwatch.Stop();
            var downloadTime = stopwatch.ElapsedMilliseconds;

            return MBsCalc(downloadTime, 100);
        }
        public async Task<double> UploadSpeedTest(string uploadUrl)
        {
            var httpClient = new HttpClient();
            var stopwatch = new Stopwatch();
            int MB = 100;

            byte[] randomData = GenerateRandomData(MB * 1024 * 1024); // 100MB

            // Create ByteArrayContent from the random data
            ByteArrayContent dataContent = new ByteArrayContent(randomData);

            stopwatch.Start();
            // Perform the HTTP POST request
            HttpResponseMessage response = await httpClient.PostAsync(uploadUrl, dataContent);
            stopwatch.Stop();
            var uploadTime = stopwatch.ElapsedMilliseconds;

            return MBsCalc(uploadTime, MB);
        }
        public async Task<long> PingTime(string targetHost)
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = await ping.SendPingAsync(targetHost);

                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                else
                {
                    return -1;
                }
            }
            catch (PingException ex)
            {
                return -1;
            }
        }

        private double MBsCalc(long ms, int MB)
        {
            return MB / ms / 1000;
        }
        private static byte[] GenerateRandomData(int size)
        {
            byte[] data = new byte[size];
            new Random().NextBytes(data);
            return data;
        }
    }
}
