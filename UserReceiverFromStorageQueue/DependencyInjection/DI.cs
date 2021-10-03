using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace UserSenderToStorageQueue.DependencyInjection
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
