using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            
            // Addind the connection of the COnnectionStrings that we specify in the appSettings.Development.json
            // It is going to be alive for the timeline of the request because of the AddDbContext<>
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));


            // We are adding to the service , so we will use interfaces name for injecting, not class name
            // We will use AddScoped because it will be created and keep as it is needed
            // We could use AddTransient -> but it is too short, it creates a method and then destry after usage
            // We could use AddSingleton -> but it is too long, it created from beginiing and destroy when everything is finished, which is too long

            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            // Add the Automapper
            // We need to specify the file (assembly file) where are mapping is happening
            services.AddAutoMapper(typeof(MappingProfiles));
           
        }


        // Order is very important in the Configure method

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(); // Call the static pictures that we add to the project

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
