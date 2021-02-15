using EmployeeManagementSystem.ADLibs;
using EmployeeManagementSystem.ADLibs.Interfaces;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using EmployeeManagementSystem.Data.SQLiteLayer.Commands;
using EmployeeManagementSystem.Data.SQLiteLayer.Queries;
using EmployeeManagementSystem.ReSTapi.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EmployeeManagementSystem.ReSTapi
{
    public class Startup
    {

        static readonly string Description = "The ReST API of the Employee Management System (EMS). It is used to administer the users and groups of an Active Directory.";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            RegisterSwagger(services);
            RegisterMappers(services);
            RegisterDatabase(services);
            RegisterActiveDirectory(services);
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EMS ReST API",
                    Version = "v1",
                    Description = Description,
                    Contact = new OpenApiContact
                    {
                        Email = "dmitrij.patuk@whatever.de",
                        Name = "Dmitrij Patuk",
                        Url = new System.Uri("https://github.com/DmitrijP"),
                    }
                });

            });
        }

        private static void RegisterMappers(IServiceCollection services)
        {
            services.AddSingleton<EmployeeMapper>();
            services.AddSingleton<GroupMapper>();
            services.AddSingleton<GroupMembershipMapper>();
        }

        private void RegisterActiveDirectory(IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>(c => new UserManager(
                Configuration["ActiveDirectoryManager:Username"], Configuration["ActiveDirectoryManager:Password"], 
                Configuration["ActiveDirectoryManager:Domain"], Configuration["ActiveDirectoryManager:UserPath"]));
            
            services.AddScoped<IGroupManager, GroupManager>(c => new GroupManager(Configuration["ActiveDirectoryManager:Username"],
                Configuration["ActiveDirectoryManager:Password"], Configuration["ActiveDirectoryManager:Domain"], 
                Configuration["ActiveDirectoryManager:GroupPath"], Configuration["ActiveDirectoryManager:UserPath"]));
        }

        private void RegisterDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddScoped<IEmployeeQueries, EmployeeQueries>(c => new EmployeeQueries(connectionString));
            services.AddScoped<IEmployeeCommands, EmployeeCommands>(c => new EmployeeCommands(connectionString));
            services.AddScoped<IGroupCommands, GroupCommands>(c => new GroupCommands(connectionString));
            services.AddScoped<IGroupQueries, GroupQueries>(c => new GroupQueries(connectionString));
            services.AddScoped<IGroupMembershipCommands, GroupMembershipCommands>(c => new GroupMembershipCommands(connectionString));
            services.AddScoped<IGroupMembershipQueries, GroupMembershipQueries>(c => new GroupMembershipQueries(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMS ReSTapi v1"));
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
