using Common.Repositories;
using Confluent.Kafka;
using System.Net;
using System.Net.Mail;
using Common.Kafka.Consumer;
using Common.Kafka.Producer;
using Domain.Notifications.Messages;
using Domain.Orders;
using DomainServices.Notifications.Hosted;
using DomainServices.Notifications.Kafka;
using DomainServices.Orders.Hosted;
using Infrastructure.Notifications;
using Infrastructure.Notifications.Repositories;
using Infrastructure.Orders.Repositories;
using Infrastructure.Orders.Rss.Parser;
using Infrastructure.Orders.Rss.Reader;
using Infrastructure.Users.Repositories;
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
            services.AddSingleton<UsersRepository>();
            services.AddSingleton<NotificationsFabric>();
            services.AddSingleton<OutboxNotificationsRepository>();
            
            services.AddHostedService<PullUnhandledOrders>();
            services.AddHostedService<HandleOrders>();
            services.AddHostedService<PushNotificationsToKafka>();
            
            services.AddSingleton(p => new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("", "")
            });
            
            services.AddDbContext<Context>(
                options => options.UseNpgsql("Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A", p => p.MigrationsAssembly(typeof(Context).Assembly.FullName)));
            services.AddSingleton<ISqlConnectionFactory>(
                p => new SqlConnectionFactory("Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A"));

            AddKafka(services);
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

        private void AddKafka(IServiceCollection services)
        {
            services
                .AddKafkaConsumer<string, Order, CreateNotificationsFromOrder>()
                .Configure<KafkaConsumerConfig<string, Order>>(p =>
                {
                    p.Topic = "orders";
                    p.GroupId = "orders_web_dev";
                    p.BootstrapServers = "localhost:9092";
                    p.AutoOffsetReset = AutoOffsetReset.Earliest;
                    p.EnableAutoOffsetStore = false;
                });

            services
                .AddKafkaConsumer<string, EmailMessage, SendEmail>()
                .Configure<KafkaConsumerConfig<string, Order>>(p =>
                {
                    p.Topic = "email";
                    p.GroupId = "email_web_dev";
                    p.BootstrapServers = "localhost:9092";
                    p.AutoOffsetReset = AutoOffsetReset.Earliest;
                    p.EnableAutoOffsetStore = false;
                });

            services
                .AddKafkaProducer<string, Order>()
                .Configure<KafkaProducerConfig<string, Order>>(p =>
                {
                    p.Topic = "orders";
                    p.BootstrapServers = "localhost:9092";
                });
        }
    }
}