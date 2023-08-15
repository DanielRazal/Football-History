global using Server.Context;
global using Microsoft.EntityFrameworkCore;
global using Server.Enums;
global using Server.Models.Inside;
// global using System.Text.Json;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Server.Repositories.Inside_Interfaces;
global using Server.Models.Outside;
global using Server.Attributes;
global using Server.Repositories.Inside;
global using Server.Repositories.Outside_Interfaces;
global using Server.ViewModels;
global using Server.Services.Interfaces;
global using Server.Services.Classes;
global using dotenv.net;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Server.Repositories.Outside;
// using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("CorsPolicy", options =>
    {
        options.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .WithOrigins(builder.Configuration["Cors:Angular"]!)
            .AllowCredentials();

    });
});

var configuration = builder.Configuration;
configuration.AddUserSecrets<Program>();

var tokenKeyValue = configuration["TokenKey"];
var key = !string.IsNullOrEmpty(tokenKeyValue) ? Encoding.ASCII.GetBytes(tokenKeyValue) : null;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddDistributedMemoryCache();


builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUploadPhotos, UploadPhotos>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<ICompletedQuizRepository, CompletedQuizRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();


builder.Services.AddDbContext<ServerContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ServerContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    var hostingEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    string uploadsPath = Path.Combine(hostingEnvironment.WebRootPath, "uploads");

    if (Directory.Exists(uploadsPath))
    {
        Directory.Delete(uploadsPath, true);
    }
    Directory.CreateDirectory(uploadsPath);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

// app.MapHub<ChatHub>("/chatHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
