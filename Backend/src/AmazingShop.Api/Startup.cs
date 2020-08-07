using AmazingShop.Api.Errors;
using AmazingShop.Api.Extensions;
using AmazingShop.Api.Helpers;
using AmazingShop.Api.Middlewares;
using AmazingShop.Core.Interfaces;
using AmazingShop.Infrastructure;
using AmazingShop.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace AmazingShop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AmazingShopContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddApplicationServices();
            services.AddSwaggerServices();
            services.AddCors(opt=>{
                opt.AddPolicy("CorsPolicy", options=>{
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwaggerServices();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
