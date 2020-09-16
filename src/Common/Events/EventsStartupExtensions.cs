using Common.Events;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventsStartupExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(IEventBus<,>), typeof(EventBus<,>));

            return serviceCollection;
        }
    }
}