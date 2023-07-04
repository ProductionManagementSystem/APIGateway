using Commons.UnitOfWork.Extensions.MySQL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistent
{
    public static class PersistentServiceCollectionExtensions 
    {
        public static IServiceCollection AddPersistentServices(this IServiceCollection services, string connectionString)
        {
            services.AddUnitOfWorkWithMySQL(connectionString);
            return services;
        }
    }
}
