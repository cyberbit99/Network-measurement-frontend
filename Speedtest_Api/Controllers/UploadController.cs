using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Speedtest_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> TestUpload()
        {
            try
            {
                var stopwatch = new Stopwatch();
                var fileSize = 0;

                using (var memoryStream = new MemoryStream())
                {
                    var buffer = new byte[4096];
                    int bytesRead;

                    stopwatch.Start();

                    while ((bytesRead = await Request.Body.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                        fileSize += bytesRead;
                    }

                    stopwatch.Stop();
                }

                double speedMbps = fileSize / stopwatch.Elapsed.TotalSeconds / 1e6; // Convert to Mbps

                return Ok(new { SpeedMbps = speedMbps });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
