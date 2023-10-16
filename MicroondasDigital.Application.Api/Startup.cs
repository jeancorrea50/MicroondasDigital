using MicroondasDigital.Data.Repository;
using MicroondasDigital.Domain.Extensions.Hubs;
using MicroondasDigital.Domain.Interfaces.Repository;
using MicroondasDigital.Domain.Interfaces.Services;
using MicroondasDigital.Domain.Security;
using MicroondasDigital.Service.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroondasDigital.Application.Api
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
            services.AddCors();

            #region Token

            var appTokenReponse = Configuration.GetSection("BearerTokenSettings");

            services.Configure<TokenConfigurations>(appTokenReponse);

            var appTokenSettings = appTokenReponse.Get<TokenConfigurations>();
            var key = Encoding.ASCII.GetBytes(appTokenSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            #endregion

            #region Injeção de dependência - Service
            services.AddScoped<INivel1Service, Nivel1Service>();
            services.AddSingleton<INivel2Service, Nivel2Service>();
            services.AddSingleton<INivel3Service, Nivel3Service>();
            services.AddSingleton<INivel4Service, Nivel4Service>();

            #endregion

            #region Injeção de dependência - Repository
            services.AddSingleton<INivel2Repository, Nivel2Repository>();
            services.AddSingleton<INivel3Repository, Nivel3Repository>();
            services.AddSingleton<INivel4Repository, Nivel4Repository>();
            services.AddSingleton<IProgramaPreDefinidoRepository, ProgramaPreDefinidoRepository>();
            services.AddSingleton<IProgramaCustomizadoRepository, ProgramaCustomizadoRepository>();
            services.AddSingleton<IAutenticacaoRepository, AutenticacaoRepository>();

            #endregion

            services.AddSignalR();
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllersWithViews()
              .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token JWT",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroondasDigital.Application.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options =>
                     options.AllowAnyOrigin()
                      .AllowAnyMethod().AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            #region Hub SignalR

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

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
