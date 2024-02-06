using System.Text.Json.Serialization;
using App.Infrastructure;
using App.Logic;
using Serilog;
using Serilog.Context;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var elasticsearchUrl = builder.Configuration["Logging:Elasticsearch:Url"];
        var autoRegisterTemplate = builder.Configuration.GetValue<bool>("Logging:Elasticsearch:AutoRegisterTemplate");
        var indexFormat = builder.Configuration["Logging:Elasticsearch:IndexFormat"];
        
        builder.Services.AddInfrastructureServices(connectionString, elasticsearchUrl, autoRegisterTemplate, indexFormat);
        builder.Services.AddLogicServices();
        
        builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        app.Use(async (context, next) =>
        {
            var correlationId = context.Request.Headers["Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await next.Invoke();
            }
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(conf => {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json","Sample Api v1");
                conf.RoutePrefix = string.Empty;
            });
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}