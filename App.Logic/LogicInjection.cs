using MediatR;

namespace App.Logic;

public static class LogicInjection
{
    public static void AddLogicServices(this IServiceCollection services)
    { 
        services.AddMediatR(typeof(LogicInjection).Assembly);
    }
}