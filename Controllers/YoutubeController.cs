using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoLibrary;

namespace YoutubeDownloader.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class YoutubeController : ControllerBase
    {

        [HttpGet("[action]")]
        public async Task<IActionResult> Download(string link)
        {
            try
            {
                var youtube = YouTube.Default;
                var video = await Task.Run(() => youtube.GetVideo(link));

                if (video == null)
                {
                    return NotFound("Video not found.");
                }

                return File(video.GetBytes(), "video/mp4", $"{video.FullName}");
            }
            catch (Exception ex)
            {
                // Log the exception details for analysis
                Console.WriteLine($"Exception: {ex.Message} , {ex.InnerException}");
                return BadRequest("An error occurred while processing your request.");
            }
        }


    }
}
