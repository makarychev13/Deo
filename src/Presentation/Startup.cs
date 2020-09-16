using System.Net;
using System.Net.Mail;

using Common.Repositories;

using Domain.Notifications;
using Domain.Orders;

using DomainServices.Notifications.Commands.SendToEmail;
using DomainServices.Notifications.Kafka.Contracts;

using Infrastructure.Notifications.BackgroundServices;
using Infrastructure.Notifications.KafkaProducers;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Migrations;

using Presentation.Ports.BackgroundServices.Orders;
using Presentation.Ports.Kafka.Notifications;
using Presentation.Ports.Kafka.Orders;

using Telegram.Bot;

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
            services.AddMediatR(typeof(SendToEmailCommand));

            services.AddInfrastructure();

            services.AddHostedService<PullOrdersBackgroundService>();
            services.AddHostedService<FlushOrdersBackgroundService>();
            services.AddHostedService<FlushNotificationsBackgroundService>();

            services.AddSingleton(
                p => new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("", "")
                });

            services.AddSingleton<ITelegramBotClient>(
                p =>
                    new TelegramBotClient("1274241939:AAHeDslxvGOlEDRFcyW9wlQW9qc31ZBxJZw"));

            services.AddDbContext<Context>(
                options => options.UseNpgsql(
                    "Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A",
                    p => p.MigrationsAssembly(typeof(Context).Assembly.FullName)));
            services.AddSingleton<ISqlConnectionFactory>(p => new SqlConnectionFactory("Server=localhost;Database=deo;User Id=postgres;Password=lthtdentgkj1A"));

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
            services.AddKafkaConsumer<string, Order, OrdersHandler>(
                p =>
                {
                    p.Topic = "orders";
                    p.GroupId = "orders_web_dev";
                    p.BootstrapServers = "localhost:9092";
                });

            services.AddKafkaConsumer<string, EmailNotification, EmailNotificationHandler>(
                p =>
                {
                    p.Topic = "email";
                    p.GroupId = "email_web_dev";
                    p.BootstrapServers = "localhost:9092";
                    p.Active = false;
                });

            services.AddKafkaConsumer<string, TelegramNotification, TelegramNotificationHandler>(
                p =>
                {
                    p.Topic = "telegram";
                    p.GroupId = "telegram_web_dev";
                    p.BootstrapServers = "localhost:9092";
                });

            services.AddKafkaProducer<string, Order>(
                p =>
                {
                    p.Topic = "orders";
                    p.BootstrapServers = "localhost:9092";
                });

            services.AddKafkaProducer<string, Notification, NotificationKafkaProducer>(p => { p.BootstrapServers = "localhost:9092"; });
        }
    }
}