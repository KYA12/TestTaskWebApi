using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestAppWebApi.Models;
using TestAppWebApi.DataAccess.UnitOfWork;
using TestAppWebApi.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TestAppWebApi.DataAccess.Repository;

namespace TestAppWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключение базы данных MS SQL Server
            var connection = Configuration.GetConnectionString("ShopDataBaseContext");
            services.AddDbContext<ShopDataBaseContext>(options => options.UseSqlServer(connection));
            
            // Внедрение зависимости UnitOFWork
            services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();
            services.AddScoped<IConsultantRepository, ConsultantRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            // Внедрение зависимости Service
            services.AddScoped<IService, Service>();
            
            // Подключение Swagger для описания и тестирования api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Подключение статических файлов и файлов по умолчанию для отображения index.html
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestTask");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
