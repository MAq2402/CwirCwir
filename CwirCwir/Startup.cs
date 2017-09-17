using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using CwirCwir.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CwirCwir.Entities;
using CwirCwir.Services;
using Microsoft.AspNetCore.Identity;
using CwirCwir.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CwirCwir
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                                       .SetBasePath(env.ContentRootPath)
                                       .AddJsonFile("appsettings.json")
                                       .AddEnvironmentVariables();



            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IResponseService, ResponseService>();
            services.AddTransient<IResponseLikeService, ResponseLikeService>();
            services.AddTransient<ICwirCwirDbContextService, CwirCwirDbContextService>();
            services.AddDbContext<CwirCwirDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("CwirCwir")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CwirCwirDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

           
            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseMvc(ConfigureRoutes);

            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigureRoutes(IRouteBuilder obj)
        {
            obj.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
