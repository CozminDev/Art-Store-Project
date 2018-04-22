using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtStore.Data;
using ArtStore.Data.Entities;
using ArtStore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Art_Store
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
               
            services.AddTransient<INullMailService, NullMailService>();
            services.AddDbContext<ArtContext>(cfg=> {
                cfg.UseSqlServer(_config.GetConnectionString("ArtConnectionString"));

            });
            services.AddTransient<ArtSeeder>();
            services.AddScoped<IArtRepository,ArtRepository>();
            services.AddAutoMapper();
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ArtContext>();
            services.AddAuthentication().AddCookie().AddJwtBearer(cfg=> 
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                };

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller="App", action="Index" });
                    
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<ArtSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}
