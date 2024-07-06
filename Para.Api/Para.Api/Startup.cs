using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Data.Context;

namespace Para.Api;

public class Startup
{
    public IConfiguration Configuration;
    
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
        });


        var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<ParaSqlDbContext>(options => options.UseSqlServer(connectionStringSql));
        
        
        var connectionStringPostgre = Configuration.GetConnectionString("PostgresSqlConnection");
        services.AddDbContext<ParaPostgreDbContext>(options => options.UseNpgsql(connectionStringPostgre));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}