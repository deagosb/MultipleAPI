using AutoMapper;
using ClientWebApi.Mappers;
using ClientWebApi.Models;
using ClientWebApi.Services;
using ClientWebApi.Services.Deal;
using ClientWebApi.Services.Deal.Validations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace ClientWebApi
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
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddControllers();            

            // Custom Services
            services.AddTransient<IApiService, ApiService>();
            services.AddScoped<IRequestValidator<PostBestDealRequest>, PostBestDealRequestValidator>();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
