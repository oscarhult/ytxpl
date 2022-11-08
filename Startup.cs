using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using YoutubeExplode;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services
      .AddHttpClient()
      .AddMemoryCache()
      .AddSingleton<YoutubeClient>()
      .AddControllers()
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
      });
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
