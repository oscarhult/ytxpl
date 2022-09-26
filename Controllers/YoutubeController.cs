using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

[ApiController, Route("api/youtube")]
public class YoutubeController : ControllerBase
{
  private readonly YoutubeClient _yt;

  public YoutubeController(YoutubeClient yt)
  {
    _yt = yt;
  }

  [HttpGet("video/{id}")]
  public async Task<ActionResult> GetVideo(string id)
  {
    try
    {
      var videoId = VideoId.Parse(id);
      var video = await _yt.Videos.GetAsync(videoId);
      var manifest = await _yt.Videos.Streams.GetManifestAsync(videoId);
      var stream = manifest.GetMuxedStreams().GetWithHighestVideoQuality();
      return Ok(new
      {
        Success = true,
        video.Title,
        video.Author.ChannelTitle,
        video.UploadDate,
        video.Duration,
        stream.Url
      });
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
