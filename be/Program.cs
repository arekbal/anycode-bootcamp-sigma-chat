using be.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");
builder.Services.AddSignalR();


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

app.MapGet("/", () => "Hello <anycode-bootcamp-sigma-chat>!");

app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run();
