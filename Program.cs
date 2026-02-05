var appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Services.AddControllers();
appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddOpenApi();

var wikiApp = appBuilder.Build();

if (wikiApp.Environment.IsDevelopment())
{
    wikiApp.MapOpenApi();
}

wikiApp.UseDefaultFiles();
wikiApp.UseStaticFiles();

wikiApp.MapControllers();
wikiApp.MapFallbackToFile("index.html");

wikiApp.Run();
