using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyProFile.Data;
using MyProFile.Data.Models;
using MyProFile.Server.Utilities;
using System.Text;

namespace MyProFile.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("[APP] Starting MyProFile...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IEmailSender, MailHelper>();
            builder.Services.AddScoped<JwtService>();

            builder.Services.AddDbContext<MyProFileDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<MyProFileDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                        {
                            context.Token = token.Substring("Bearer ".Length).Trim();
                            Console.WriteLine($"[JWT] ✅ Получен токен: {context.Token.Substring(0, 25)}...");
                        }
                        else
                        {
                            Console.WriteLine("[JWT] ⚠️  Липсва Authorization header или не започва с 'Bearer '");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("[JWT] ❌ Authentication failed:");
                        Console.WriteLine(context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });

            var app = builder.Build();

            Console.WriteLine("[APP] Environment: " + app.Environment.EnvironmentName);
            Console.WriteLine("[APP] Running on: https://localhost:7082");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}