using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Domain.Orders;
using DomainServices.Orders;
using Infrastructure.Common.Kafka;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Presentation
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
            services.AddSingleton<IOrdersReader>(new OrdersReader(
                new OrdersParser(),
                new Uri("https://freelance.ru/rss/projects.xml"), "freelance.ru"));
            services.AddHostedService<OrdersService>();
            services
                .AddKafkaConfigs(new ProducerConfig() {BootstrapServers = "localhost:9092"})
                .AddKafkaProducer<string, Order>("orders");
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}