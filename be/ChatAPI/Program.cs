using ChatAPI.Hubs;
using Neuroglia.AsyncApi;
using Neuroglia.AsyncApi.Models.Bindings.WebSockets;
using Newtonsoft.Json.Schema;
using System.Text.Json;
using Saunter;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

var services = builder.Services;
services.AddSignalR();
services.AddAsyncApiGeneration(options =>
{
  options
  .WithMarkupType<ChatHub>()
  .UseDefaultConfiguration(asyncapi =>
  {
    asyncapi.UseServer("WebSocket", server => server
          .WithUrl(new Uri("http://127.0.0.1:8081/ws/weather"))
          .WithProtocol(AsyncApiProtocols.Ws)
          .UseBinding(new WsServerBinding())
          );
  });
});

services.AddAsyncApiUI();
services.AddRazorPages();
//services.AddSingleton<IJsonSchemaResolver, JsonSchemaResolver>();
services.AddHttpClient();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(b =>
  {
    b.WithOrigins("http://localhost:3000")
          .AllowAnyHeader()
          .WithMethods("GET", "POST");
  });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapAsyncApiDocuments();

app.MapGet("/", () => "Hello <anycode-bootcamp-sigma-chat>!");

app.MapAsyncApiDocuments();
app.MapAsyncApiUi();

app.MapControllers();

app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run();
