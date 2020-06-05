using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Occam.Core;

namespace Occam
{
    public static class ServicesCollectionExtensions
    {        
        public static IServiceCollection AddOccam(this IServiceCollection services)
        {
            services.AddPatchedService<IHost, OccamHost>();
            return services;
        }       
        private static IServiceCollection AddPatchedService<TService>(this IServiceCollection services, Func<TService, IServiceProvider, TService> factory)
            where TService: class
        {
            var orig = services.FirstOrDefault(sd => sd.ServiceType == typeof(TService))
                ?? throw new InvalidOperationException($"Service {typeof(TService).Name} does not have a pre-existing implementation.");
            services.RemoveAll<TService>();
            Func<IServiceProvider, TService> origFactory;
            if (orig.ImplementationInstance != null) // Instance
                origFactory = ioc => orig.ImplementationInstance as TService;
            else if (orig.ImplementationFactory != null) // Factory
                origFactory = ioc => orig.ImplementationFactory(ioc) as TService;
            else if (orig.ImplementationType != null) // Type
                origFactory = ioc => ActivatorUtilities.CreateInstance(ioc, orig.ImplementationType) as TService;
            else
                throw new InvalidOperationException($"Service {typeof(TService).Name} pre-existing implementation cannot be replicated.");
            services.Add(new ServiceDescriptor(typeof(TService), ioc => factory.Invoke(origFactory(ioc), ioc), orig.Lifetime));
            return services;
        }
        private static IServiceCollection AddPatchedService<TService, TImplementation>(this IServiceCollection services)
            where TService: class
            where TImplementation: TService
        {
            return services.AddPatchedService<TService>((origImpl, services)
                => ActivatorUtilities.CreateInstance<TImplementation>(services, origImpl));
        }
    }
}
