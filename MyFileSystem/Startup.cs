using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyFileSystem.Core.Entities;
using MyFileSystem.Persistence;
using MyFileSystem.Services.File;
using MyFileSystem.Services.Interfaces.File;
using System;
using System.IO;
using System.Text;

namespace MyFileSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
          

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<FileSystemDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().AddFluentValidation();
            services.AddScoped<IFileManager, OSFileManager>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireDigit").Value);
                options.Password.RequireLowercase = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireLowercase").Value);
                options.Password.RequireUppercase = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireUppercase").Value);
                options.Password.RequireNonAlphanumeric = Boolean.Parse(Configuration
                    .GetSection("IdentityConfiguration").GetSection("RequireNonAlphanumeric").Value);
                options.Password.RequiredLength = int.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequiredLength").Value);
            })
                .AddEntityFrameworkStores<FileSystemDbContext>()
                .AddDefaultTokenProviders();


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.ClaimsIssuer = Configuration["Jwt:Issuer"];
            //    options.Audience = Configuration["Jwt:Audience"];
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidIssuer = Configuration["Issuer"],
            //        ValidAudience = Configuration["Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"])),
            //        
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true
            //    };
            //});

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.ClaimsIssuer = Configuration["Jwt:Issuer"];
                options.Audience = Configuration["Jwt:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"])),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(options =>
            {
                services.AddSwaggerGen();
            });


            //services.AddScoped<IFolderService, FolderService>();
            //services.AddScoped<IFileService, FileService>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses()
            .AsMatchingInterface());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { app.UseDeveloperExceptionPage(); }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers();});

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
         
        }
    }
}
