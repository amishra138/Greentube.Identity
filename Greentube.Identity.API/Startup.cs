using Greentube.Identity.API.Handler.Queries;
using Greentube.Identity.API.Handler.RequestHandlers;
using Greentube.Identity.Domain.Requests;
using Greentube.Identity.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Greentube.Identity.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<GreentubeUserContext>(config => config.UseInMemoryDatabase("GreentubeInMemoryDb"));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Greentube Identity API", Version = "v1", Description = "A sample API to manage user identity" });
            });

            //AddIdentity registers the services
            services
                .AddIdentity<IdentityUser, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.User.RequireUniqueEmail = true;
                    config.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<IdentityUser>)));
                })
                .AddEntityFrameworkStores<GreentubeUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.Name = "Default";
                opt.TokenLifespan = TimeSpan.FromMinutes(Configuration.GetValue<int>("LinkExpiryTimeInMinutes"));
            });

            services.AddControllers();

            services.AddScoped<IUserQuery, UserQuery>();

            services.AddMediatR(typeof(ForgotPasswordRequest), typeof(ForgotPasswordRequestHandler));

            services.AddInfrastructureServices();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Greentube Identity API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
