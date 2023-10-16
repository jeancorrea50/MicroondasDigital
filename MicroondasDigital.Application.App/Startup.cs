using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Service;
using MicroondasDigital.Domain.Extensions.Hubs;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Data.Repository;
using MicroondasDigital.Service.App;

namespace MicroondasDigital.Application.App
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
            #region Injeção de dependência - Service
            services.AddSingleton<INivel1Service, Nivel1Service>();
            services.AddSingleton<INivel2Service, Nivel2Service>();
            services.AddSingleton<INivel3Service, Nivel3Service>();
            #endregion

            #region Injeção de dependência - Repository
            services.AddSingleton<IProgramaPreDefinidoRepository, ProgramaPreDefinidoRepository>();
            services.AddSingleton<IProgramaCustomizadoRepository, ProgramaCustomizadoRepository>();
            services.AddSingleton<INivel2Repository, Nivel2Repository>();
            services.AddSingleton<INivel3Repository, Nivel3Repository>();
            #endregion

            services.AddControllersWithViews();

            services.AddSignalR();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. 
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<Nivel1Hub>("/nivel1Hub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<Nivel2Hub>("/nivel2Hub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<Nivel3Hub>("/nivel3Hub");
            });

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Nivel1}/{action=Index}/{id?}");
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Nivel2}/{action=Index}/{id?}");
                });
            });
        }
    }
}
