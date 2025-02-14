using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using SignSafe.Ioc;
using SignSafe.Presentation.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithCorrelationId(addValueIfHeaderAbsence: true)
    .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {UserId} {Event} - {Message:l}{NewLine}{Exception}")
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {UserId} {Event} - {Message:l}{NewLine}{Exception}")
    .WriteTo.Seq(serverUrl: "http://signsafe.seq:5341")
    );

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insert the token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[]{}
        }
    });

});

//Add DependencyInjection
DependencyInjection.AddDependencyInjection(builder);

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseExceptionHandler("/Error");

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
