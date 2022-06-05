using GeoApi;
using GeoApi.Services;
using Microsoft.OpenApi.Models;
using Sample.Cache;
using Sample.Core;
using Sample.Core.Logging;
using Sample.Data;
using Sample.Data.Migrator;
using Sample.Data.Repositories;
using Sample.Data.Repositories.Interfaces;
using Sample.Rest;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using EnvironmentName = Sample.Core.EnvironmentName;
using ILogger = Microsoft.Extensions.Logging.ILogger;


var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment.EnvironmentName.ToEnvironmentNameEnum();


// Logging
var logLevel = env == EnvironmentName.Dev ? LogEventLevel.Debug : LogEventLevel.Information;
var log = new StructuredLog();
log.Init(logLevel, env);

builder.Services.AddLogging(logBuilder =>
{
    logBuilder
        .ClearProviders()
        .AddSerilog(log.Logger)
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
});

var logFactory = new SerilogLoggerFactory(log.Logger);
builder.Services.AddSingleton(logFactory);


// Injected Services Setup
builder.Services.AddSingleton<IRestClient, RestClient>();
builder.Services.AddSingleton<ISecrets, Secrets>();
builder.Services.AddTransient<IIpLookupService, IpLookupService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IKeyValueCache>(sp => new InMemoryCache("SampleApp", sp.GetRequiredService<IDateTimeProvider>()));
builder.Services.AddTransient<ILogger>(sp => sp.GetRequiredService<SerilogLoggerFactory>().CreateLogger("default") );

/*  We'd normally load these from whatever Secret Provider we preferred but for the sake of time we'll save them here
 * for the sample app. The passwords all exist in other files (migrator scripts/docker compose config etc) so
 * it's not a big deal. As this is a sample app we also don't care about supporting other envs */
var dbSecrets = env == EnvironmentName.Dev ? new DbSecrets(
    "Server=localhost;Port=54345;Database=sampleappdb;User Id=sampleapp_ro;Password=skincare-evasion-grove-tavern-recapture-overlaid;",
    "Server=localhost;Port=54345;Database=sampleappdb;User Id=sampleapp;Password=ecosystem-endless-vowel-stadium-attic-cufflink;",
    "Server=localhost;Port=54345;Database=postgres;User Id=postgres;Password=earful-obtain-estranged-whiny-enjoyer-graceful;")
        : throw new NotImplementedException("DB Conn Strings are not implemented in other environments");
builder.Services.AddSingleton<IDbSecrets>(dbSecrets);
builder.Services.AddSingleton<IDbMigrator, DbMigrator>();
builder.Services.AddSingleton<IIpLookupRepository, IpLookupRepository>();
builder.Services.MigrateDb();

// Finish setting up the WebAPI bits
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "IP Geo-Location Lookup API",
        Description = "Provides geo-location related functionality",
                
        Contact = new OpenApiContact
        {
            Email = "17553415+gary-lg@users.noreply.github.com",
            Name = "Leaping Gorilla LTD"
        },
                
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();