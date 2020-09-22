using DbUp;
using DbUp.Engine;

using DomainServices.Orders.Commands.PullOrders;

using Infrastructure;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Presentation.Orders.Ports.BackgroundServices;

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
            services.AddMediatR(typeof(PullOrdersCommand));

            services.AddControllers();

            services.AddInfrastructure();

            services.AddHostedService<PullOrdersBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            MigrationApply("Server=localhost;Database=postgres;User Id=postgres;Password=postgres", logger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void MigrationApply(string connectionString, ILogger logger)
        {
            UpgradeEngine upgrader =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsFromFileSystem("../Migrations/Scripts")
                    .LogToConsole()
                    .Build();

            DatabaseUpgradeResult result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                logger.LogError("Не удалось накатить миграции", result.Error);
            }
        }
    }
}