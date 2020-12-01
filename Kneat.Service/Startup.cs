using Kneat.Service.Domain;
using Kneat.Service.Domain.Interface;
using Kneat.Service.Domain.Providers;
using Kneat.Service.Repository.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kneat.Service
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
            services.AddControllers();
            services
                .AddTransient<IStarShipService, StarShipService>()
                .AddTransient<IHttpClientProvider>(_ => new HttpClientProvider(new System.Net.Http.HttpClient()))
                .AddTransient<IStarWarsRepository>(_ => new StarWarsRepository(new StarWarsRepositoryHelper(new HttpClientProvider(new System.Net.Http.HttpClient()))))
                .AddTransient<IStarWarsRepositoryHelper>(_ => new StarWarsRepositoryHelper(new HttpClientProvider(new System.Net.Http.HttpClient())));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
