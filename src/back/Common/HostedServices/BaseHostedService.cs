using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace Common.HostedServices
{
    public abstract class BaseHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        protected abstract TimeSpan Period { get; }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await ExecuteAsync(), null, TimeSpan.Zero, Period);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected abstract Task ExecuteAsync();
    }
}