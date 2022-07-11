using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // We are adding to the service , so we will use interfaces name for injecting, not class name
            // We will use AddScoped because it will be created and keep as it is needed
            // We could use AddTransient -> but it is too short, it creates a method and then destry after usage
            // We could use AddSingleton -> but it is too long, it created from beginiing and destroy when everything is finished, which is too long

            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));


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
            return services;
        }
    }
}