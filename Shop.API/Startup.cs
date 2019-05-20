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
using AutoMapper;
using Shop.DAL.Helpers;
using Microsoft.EntityFrameworkCore;
using Shop.BLL.Interfaces;
using Shop.BLL.Implementations;
using Shop.DAL.Interfaces;
using Shop.DAL.Implementations;

namespace Shop.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper();
#pragma warning restore CS0618 // Type or member is obsolete

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Shop;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<EFDbContext>
                (options => {
                    options.UseSqlServer(connection);
                    //options.UseInMemoryDatabase(databaseName: "InMemoryDb");
                });

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
