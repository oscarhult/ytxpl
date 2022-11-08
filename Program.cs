using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

WebHost
  .CreateDefaultBuilder()
  .UseStartup<Startup>()
  .Build()
  .Run();
