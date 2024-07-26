using AspisNet.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspisNet.Extensions;

public static class IServiceCollectionExtensions {

    /// <summary>
    /// Automaatically register all repositories in an assembly.
    /// (Method based on article found in https://medium.com/@josiahmahachi/using-reflection-to-register-repositories-in-net-core-ebbc32f2d0ae)
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <exception cref="InvalidOperationException">Repository must implement only one interface that implements IRepositoryBase</exception>
    public static void AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        IEnumerable<Type>? repositoryTypes = assembly.GetTypes()
            .Where(type => !type.IsAbstract && !type.ContainsGenericParameters && !type.IsInterface && type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<,>)));

        foreach (Type repositoryType in repositoryTypes)
        {
            List<Type> interfaces = repositoryType.GetInterfaces()
                .Where(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRepository<,>))
                .ToList();

            if (interfaces.Count != 1)
            {
                throw new InvalidOperationException($"Repository '{repositoryType.Name}' must implement only one interface that implements Repository<T>.");
            }

            services.AddScoped(interfaces[0], repositoryType);
        }
    }

}
