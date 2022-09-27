using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

[ApiController, Route("api/youtube")]
public class YoutubeController : ControllerBase
{
  private readonly YoutubeClient _yt;
  private readonly IMemoryCache _mc;

  public YoutubeController(YoutubeClient yt, IMemoryCache mc)
  {
    _yt = yt;
    _mc = mc;
  }

  [HttpGet("video/{id}")]
  public async Task<ActionResult> GetVideo(string id)
  {
    try
    {
      var videoId = VideoId.Parse(id);
      var res = await _mc.GetOrCreateAsync(videoId.Value, async x =>
      {
        x.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
        var video = await _yt.Videos.GetAsync(videoId);
        var manifest = await _yt.Videos.Streams.GetManifestAsync(videoId);
        var stream = manifest.GetMuxedStreams().GetWithHighestVideoQuality();
        return new
        {
          Success = true,
          video.Title,
          video.Author.ChannelTitle,
          video.UploadDate,
          video.Duration,
          stream.Url
        };
      });
      return Ok(res);
    }
    catch
    {
      return Ok(new
      {
        Success = false,
        Error = "No Video Found"
      });
    }
  }
}
