using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using BasicClientServerApp.Server.Stores;
using BasicClientServerApp.Server.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using BasicClientServerApp.Server.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BasicClientServerApp.Server.Authorization;

namespace BasicClientServerApp.Server
{
    public class Startup
    {
         public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<EmployeeStore>();
            services.AddScoped<EmployeeMapper>();
            services.AddScoped<UserStore>();
            services.AddScoped<EmployeePermissionStore>();
            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddSingleton<IAuthorizationHandler, OneOrMoreRolesAuthorizationHandler>();
            services.AddAuthorization(options => 
                options.AddPolicy("OneOrMoreReadGroupPolicy", policy => 
                    policy.Requirements.Add(new OneOrMoreRoleAuthorizationRequirenment(new string[] { "RSXG-BCSApp-Read-Prod", "RSXG-BCSApp-Read-Test", "RSXG-BCSApp-Read-Dev" } ))
                ));
            services.AddScoped<IClaimsTransformation, ClaimS>();

            services.AddCors(
                options =>
                {
                    options.AddPolicy("MyCorsPolicy", builder =>
                    {
                        builder.WithOrigins("file:///C:/Users/dmitr/Documents/PlatformIO/Projects/JavascriptTutorials/*");
                    });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
