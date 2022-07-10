using System.Linq;
using API.Errors;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

            // Usually it does not matter the order here
            // but in this case matters :D
            // because we are ovrwriting the ApiController behaviour
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
           
        }


        // Order is very important in the Configure method

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                 // app.UseDeveloperExceptionPage(); => we do not use this more , because we created our middle ware, which is upper
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            // We put it upp in Configure, because it is ery important point in the application
            // How does it work, =>  when we get an error , it is redirected to this end point
            //especially when user will hit an end point that does no exist
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

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
