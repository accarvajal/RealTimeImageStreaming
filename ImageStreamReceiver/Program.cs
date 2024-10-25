using ImageStreamReceiver.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string allowedOrigin = builder.Configuration["Cors:AllowedOrigin"]!;

builder.Services.AddSignalR(options =>
{
	options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
});
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policyBuilder =>
	{
		policyBuilder.WithOrigins(allowedOrigin)
					 .AllowAnyHeader()
					 .AllowAnyMethod()
					 .AllowCredentials();
	});
});

var app = builder.Build();
app.UseCors();
app.MapHub<ImageHub>("/imageHub");
app.Run();
