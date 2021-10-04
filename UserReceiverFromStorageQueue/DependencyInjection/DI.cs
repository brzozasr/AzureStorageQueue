using Microsoft.Extensions.DependencyInjection;

namespace UserReceiverFromStorageQueue.DependencyInjection
{
    class DI
    {
        private static IServiceCollection _instance;

        private DI()
        {
        }

        public static IServiceCollection BuildServiceProvider()
        {
            return _instance ??= new ServiceCollection();
        }
    }
}
