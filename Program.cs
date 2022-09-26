using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using YoutubeExplode;

WebHost
  .CreateDefaultBuilder()
  .ConfigureServices(x =>
  {
    x.AddSingleton<YoutubeClient>();
    x.AddControllers();
  })
  .Configure(x =>
  {
    var fileProvider = new EmbeddedFileProvider(typeof(Program).Assembly, "app.Website");
    x.UseDefaultFiles(new DefaultFilesOptions { FileProvider = fileProvider });
    x.UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider });
    x.UseRouting();
    x.UseEndpoints(x => x.MapControllers());
  })
  .Build()
  .Run();