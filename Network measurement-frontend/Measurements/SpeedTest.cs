﻿using System;
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
            double ret = MBsCalc(downloadTime, (long)(response.Content.Headers.ContentLength / 1024 / 1024));

            return ret;
        }
        public async Task<double> UploadSpeedTest(string uploadUrl)
        {
            var httpClient = new HttpClient();
            var stopwatch = new Stopwatch();
            int MB = 10;

            byte[] randomData = GenerateRandomData(MB * 1024 * 1024); // 10MB

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
            var httpClient = new HttpClient();
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            HttpResponseMessage response = await httpClient.GetAsync(targetHost);
            stopwatch.Stop();
            var pingTime = stopwatch.ElapsedMilliseconds;
            return pingTime;
            //try
            //{
            //    Ping ping = new Ping();
            //    PingReply reply = await ping.SendPingAsync(targetHost);

            //    if (reply.Status == IPStatus.Success)
            //    {
            //        return reply.RoundtripTime;
            //    }
            //    else
            //    {
            //        return -1;
            //    }
            //}
            //catch (PingException ex)
            //{
            //    return -1;
            //}
        }

        private double MBsCalc(long ms, long MB)
        {
            double mbs = MB / (Convert.ToDouble(ms) / 1000);
            return Math.Round(mbs, 1);
        }
        private static byte[] GenerateRandomData(int size)
        {
            byte[] data = new byte[size];
            new Random().NextBytes(data);
            return data;
        }

        public long PingHost(string nameOrAddress)
        {
            long pingtime =0;
            Ping pinger = null;
            int times = 4;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                for ( int i = 0;  i < times; i++)
                {
                     reply =  pinger.Send(nameOrAddress);
                    Thread.Sleep(500);
                     pingtime += reply.RoundtripTime;
                }
                pingtime = pingtime / times;

            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingtime;
        }
    }
}
