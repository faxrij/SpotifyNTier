using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public static class ModelBuilderExtensions
{
    public static void ApplyAllConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
    {
        var configurations = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Any(IsIEntityTypeConfiguration))
            .Select(Activator.CreateInstance)
            .ToList();

        foreach (var configuration in configurations)
        {
            modelBuilder.ApplyConfiguration((dynamic)configuration!);
        }
    }
    private static bool IsIEntityTypeConfiguration(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);
    }
}
