using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SOA.Data;
using SOA.Models;
using SOA.Services;

namespace SOA
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            var serviceProvider = app.ApplicationServices.GetService<IServiceProvider>(); CreateRoles(serviceProvider).Wait();
        }

        //Creates roles
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //Adds customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Creates a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = "danielbrogers594@gmail.com",
                Email = "danielbrogers594@gmail.com",
            };

            string userPWD = "P@ssword1!";
            var _user = await UserManager.FindByEmailAsync("danielbrogers594@gmail.com");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //Tie the new user to the role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }

            //Creates a manager user who will creates quotes and confirms orders
            var manageruser = new ApplicationUser
            {
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
            };

            string managerPWD = "P@ssword1!";
            var _manager = await UserManager.FindByEmailAsync("manager@gmail.com");

            if (_manager == null)
            {
                var createManagerUser = await UserManager.CreateAsync(manageruser, managerPWD);
                if (createManagerUser.Succeeded)
                {
                    //Tie the new user to the role 
                    await UserManager.AddToRoleAsync(manageruser, "Manager");
                }
            }

            //Creates a member user that can request quotes and place orders
            var memberuser = new ApplicationUser
            {
                UserName = "customer@gmail.com",
                Email = "customer@gmail.com",
            };

            string memberPWD = "P@ssword1!";
            var _member = await UserManager.FindByEmailAsync("customer@gmail.com");

            if (_member == null)
            {
                var createMemberUser = await UserManager.CreateAsync(memberuser, memberPWD);
                if (createMemberUser.Succeeded)
                {
                    //Tie the new user to the role
                    await UserManager.AddToRoleAsync(memberuser, "Member");
                }
            }
        }

    }
}