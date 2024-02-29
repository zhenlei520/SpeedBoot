using SpeedBoot.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.AddMinimalAPIs();

app.MapGet("/", () => "Hello World!");

app.Run();
