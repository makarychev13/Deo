using System;
using Common.Kafka.Consumer;
using Confluent.Kafka;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Infrastructure.Orders.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddOrdersService(new FreelanceBurse(new Uri("https://kwork.ru/rss"), "kwork"), "kwork");
            services.AddOrdersService(
                new FreelanceBurse(new Uri("https://freelance.ru/rss/projects.xml"), "freelance.ru"), "freelance");
            services.AddOrdersService(
                new FreelanceBurse(new Uri("https://freelance.habr.com/rss/tasks"), "freelance.habr"), "habr");
            services
                .AddKafkaConfigs(new ProducerConfig() {BootstrapServers = "localhost:9092"})
                .AddKafkaProducer<string, Order>("orders");
            services
                .AddSingleton<IHostedService, KafkaOrdersConsumer>(p => new KafkaOrdersConsumer(
                    new KafkaConsumerOptions
                    {
                        Topic = "orders",
                        Config = new ConsumerConfig {GroupId = "dev_1", BootstrapServers = "localhost:9092"}
                    }));
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