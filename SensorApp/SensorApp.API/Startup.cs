using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SensorApp.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorApp.API.BackgroundSubscribe;
using MediatR;
using System.Reflection;
using SensorApp.API.Formater;
using SensorApp.API.Hubs;

namespace SensorApp.API
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
            //CORS
            services.AddCors(o => o.AddPolicy("Access-Control-Allow-Origin", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SensorApp.API", Version = "v1" });
            });
            services.AddMvc(options => options.OutputFormatters.Add(new CsvFormatter()));
            //Configure DataBase
            services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                });

            services.AddSingleton<IMongoSensorDBContext, MongoSensorDBContext>();
            services.AddSingleton<IRepositorySensor, RepositorySensor>();

            //Configura BackGroundService
            services.Configure<SettingsRB>(
                options =>
                {
                    options.HostName = Configuration.GetSection("RabbitMQ:HostName").Value;
                    options.Port = Configuration.GetSection("RabbitMQ:Port").Value;
                    options.UserName = Configuration.GetSection("RabbitMQ:UserName").Value;
                    options.Password = Configuration.GetSection("RabbitMQ:Password").Value;
                    options.Queue = Configuration.GetSection("RabbitMQ:Queue").Value;
                });
            services.AddHostedService<RabbitConnection>();

            //Configure Mediator
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //CORS
            app.UseCors("Access-Control-Allow-Origin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SensorApp.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SendDataHub>("/stream");
            });
        }
    }
}
