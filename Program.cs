using todo.Data;
using Microsoft.EntityFrameworkCore;
using Konso.Clients.Logging.Extensions;
using Konso.Clients.Logging.Models;
using Konso.Clients.ValueTracking.Interfaces;
using Konso.Clients.ValueTracking.Services;
using Konso.Clients.ValueTracking.Extensions;
using Microsoft.Extensions.Configuration;
using Konso.Clients.Metrics.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// logging
var _logsConfig = new KonsoLoggerConfig();
_logsConfig.Endpoint = builder.Configuration["Konso:Logging:Endpoint"];
_logsConfig.BucketId = builder.Configuration["Konso:Logging:BucketId"];
_logsConfig.AppName = builder.Configuration["Konso:Logging:App"];
_logsConfig.ApiKey = builder.Configuration["Konso:Logging:ApiKey"];
_logsConfig.LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), builder.Configuration["Konso:Logging:Level"].ToString());

builder.Services.AddSingleton(_logsConfig);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();

// adding session state management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); ;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});

builder.Logging.ClearProviders();

// configure metrics

builder.Services.ConfigureKonsoMetrics(options =>
{
    options.Endpoint = builder.Configuration["Konso:Metrics:Endpoint"];
    options.BucketId = builder.Configuration["Konso:Metrics:BucketId"];
    options.ApiKey = builder.Configuration["Konso:Metrics:ApiKey"];
    options.AppName = builder.Configuration["Konso:Metrics:App"];
});

// configure logger

builder.Logging.AddKonsoLogger(options =>
{
    options.Endpoint = builder.Configuration["Konso:Logging:Endpoint"];
    options.BucketId = builder.Configuration["Konso:Logging:BucketId"];
    options.AppName = builder.Configuration["Konso:Logging:App"];
    options.ApiKey = builder.Configuration["Konso:Logging:ApiKey"];
    options.LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), builder.Configuration["Konso:Logging:Level"].ToString());

});

// configure value tracking

builder.Services.AddKonsoValueTracking(options =>
{
    options.Endpoint = builder.Configuration.GetValue<string>("Konso:ValueTracking:Endpoint");
    options.BucketId = builder.Configuration.GetValue<string>("Konso:ValueTracking:BucketId");
    options.ApiKey = builder.Configuration.GetValue<string>("Konso:ValueTracking:ApiKey");
});

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "ToDos"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// configure session state management
app.UseSession();

// hook session id from session state for tracing
app.UseSessionLogTracing();

// measure request performance

app.UseKonsoMetrics();

app.MapRazorPages();

app.Run();
