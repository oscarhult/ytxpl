using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using YoutubeExplode;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services
      .AddHttpClient()
      .AddMemoryCache()
      .AddSingleton<YoutubeClient>()
      .AddControllers();
  }

  public void Configure(IApplicationBuilder application, IConfiguration configuration)
  {
    var fileProvider = new EmbeddedFileProvider(typeof(Program).Assembly, "app.Website");

    application
      .UseDefaultFiles(new DefaultFilesOptions { FileProvider = fileProvider })
      .UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider })
      .UseRouting()
      .UseEndpoints(x => x.MapControllers());
  }
}
