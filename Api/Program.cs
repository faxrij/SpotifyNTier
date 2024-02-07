using System.Text.Json.Serialization;
using App.Infrastructure;
using App.Infrastructure.Middlewares;
using App.Logic;
using Newtonsoft.Json;

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
        
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        
        var app = builder.Build();
        
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
        
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.MapControllers();
        
        app.Run();
    }
}