using Microsoft.OpenApi.Models;
using SpeedBoot.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        // options.SwaggerDoc(builder.Options.GlobalServiceRouteOptions.Version, builder.Options.OpenApiInfo ?? new OpenApiInfo()
        // {
        //     Title = $"{builder.Options.ProjectName}",
        //     Version = builder.Options.GlobalServiceRouteOptions.Version
        // });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            BearerFormat = "JWT",
            Scheme = "Bearer",
            Description = "Specify the authorization token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        // 支持多态
        options.UseAllOfForInheritance();
        options.UseOneOfForPolymorphism();
    });
var app = builder.AddMinimalAPIs();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
});

app.MapGet("/", () => "Hello World!");

app.Run();
