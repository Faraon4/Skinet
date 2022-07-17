using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            // Add the Automapper
            // We need to specify the file (assembly file) where are mapping is happening
            services.AddAutoMapper(typeof(MappingProfiles));

            // We did some hose keeping
            // We add some services to this class
            // and then we just add this clus here, in services
            services.AddApplicationServices();
           
           services.AddSwaggerDocumentation(); // Method that we created


           services.AddCors(opt => 
           {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
           });
        }


        // Order is very important in the Configure method

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            // We put it upp in Configure, because it is ery important point in the application
            // How does it work, =>  when we get an error , it is redirected to this end point
            //especially when user will hit an end point that does no exist
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(); // Call the static pictures that we add to the project

            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.UseSwaggerDocumentation(); // Method that we jst created 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
