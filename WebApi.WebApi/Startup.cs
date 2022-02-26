using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WebApi.DataAccess.MsSql;
using WebApi.UseCases;
using WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;
using WebApi.Utils.Modules;
using WebApi.WebApi.Utils;

namespace WebApi.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    x.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    x.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                });
            services.RegisterModule<DataAccessModule>(Configuration);
            services.RegisterModule<UseCasesModule>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
