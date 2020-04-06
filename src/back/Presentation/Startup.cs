using System;
using Common.Kafka.Consumer;
using Common.Repositories;
using Confluent.Kafka;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using DomainServices.Orders.Hosted;
using Infrastructure.Orders.Kafka;
using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Migrations;

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
            services.AddSingleton<IOrdersParser, OrdersParser>();
            services.AddSingleton<IOrdersReader, OrdersReader>();
            services.AddSingleton<OrdersRepository>();
            services.AddSingleton<FreelanceBursesRepository>();
            services.AddHostedService<PullUnhandledOrders>();
            services.AddHostedService<HandleOrders>();
            services
                .AddKafkaConfigs(new ProducerConfig() {BootstrapServers = "localhost:9092"})
                .AddKafkaProducer<string, Order>("orders");
            services
                .AddSingleton<IHostedService, KafkaOrdersConsumer>(p => new KafkaOrdersConsumer(
                    new KafkaConsumerOptions
                    {
                        Topic = "orders",
                        Config = new ConsumerConfig {GroupId = "dev_1", BootstrapServers = "localhost:9092", AutoOffsetReset = AutoOffsetReset.Earliest}
                    }));
            services.AddDbContext<Context>(
                options => options.UseNpgsql("Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A"));
            services.AddSingleton<ISqlConnectionFactory>(
                p => new SqlConnectionFactory("Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A"));
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