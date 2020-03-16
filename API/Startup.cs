using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherForecast.WebApi.Data;
using WeatherForecast.WebApi.Features.Weather.Interfaces;
using WeatherForecast.WebApi.Features.Weather.Services;

namespace WeatherForecast.WebApi
{
    public class Startup
    { 
        private const string ClientAppCors = "clientApp";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
         
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors((corsOptions) =>
            {
                corsOptions.AddPolicy(ClientAppCors, builder =>
                {
                    string clientAppUrl = Configuration.GetValue<string>("ClientAppUrl");
                    builder
                     .WithOrigins(clientAppUrl)
                     .WithMethods("GET") 
                     .Build();
                });
            });
            services.AddOptions();

            string connectionString = Configuration.GetConnectionString("DefaultConnection"); 
            services.AddEntityFrameworkNpgsql().AddDbContext<WeatherForecastContext>(options => options.UseNpgsql(connectionString));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IWeatherForecast, WeatherForecastClient>();
            services.AddScoped<IWeatherForecastResult, WeatherParser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            app.UseCors(ClientAppCors);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
