using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MyFileSystem.Persistence;
using MyFileSystem.Persistence.Repositories;
using MyFileSystem.Persistence.UnitOfWork;
using MyFileSystem.Services.File;
using MyFileSystem.Services.Folder;
using MyFileSystem.Services.Interfaces.File;
using MyFileSystem.Services.Interfaces.Folder;
using System.IO;

namespace MyFileSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddDbContext<FileSystemDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper(typeof(Startup)); 
            services.AddMvc().AddFluentValidation();
            services.AddScoped<IFolderService, FolderService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileManager, OSFileManager>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { app.UseDeveloperExceptionPage(); }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
        }
    }
}
