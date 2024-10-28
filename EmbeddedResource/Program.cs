using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();



var rewriteOptions = new RewriteOptions()
    .AddRewrite(@"^assets/(.*)", "ui/assets/$1", skipRemainingRules: true);
app.UseRewriter(rewriteOptions);


// Use static files from embedded resources
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new ManifestEmbeddedFileProvider(
        Assembly.GetExecutingAssembly(), "ui" 
    ),
    RequestPath = "/ui", 
    ServeUnknownFileTypes = true, 
    DefaultContentType = "text/plain",
    OnPrepareResponse = ctx =>
    {
        if (ctx.File.Name.EndsWith(".js"))
            ctx.Context.Response.ContentType = "application/javascript";
        else if (ctx.File.Name.EndsWith(".html"))
            ctx.Context.Response.ContentType = "text/html";
        else if (ctx.File.Name.EndsWith(".svg"))
            ctx.Context.Response.ContentType = "image/svg+xml";
    }
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

foreach (var resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
{
    Console.WriteLine(resource);
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}