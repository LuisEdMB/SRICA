using API.SRICA.Aplicacion.Implementacion.Hubs;
using API.SRICA.Distribucion.Hubs;
using API.SRICA.Infraestructura.Injector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Security.Cryptography;
using System.Text;

namespace API.SRICA.Distribucion
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(g =>
                    {
                        g.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["SEGURIDAD_ISSUER"],
                            ValidAudience = Configuration["SEGURIDAD_AUDIENCIA_PERMITIDA"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(
                                    Configuration["SEGURIDAD_CLAVE_SECRETA"]))),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            services.AddControllers().AddNewtonsoftJson(g =>
            {
                g.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            var injector = new InjectorDependencia(services);
            injector.ContenedorDependencia(
                "server=" + Configuration["BASE_DATOS_SERVIDOR"] + "; " +
                "database=" + Configuration["BASE_DATOS_CATALOGO"] + "; " +
                "user id=" + Configuration["BASE_DATOS_USUARIO"] + "; " +
                "password=" + Configuration["BASE_DATOS_CLAVE"] + "; " + 
                "port=" + Configuration["BASE_DATOS_PUERTO"]);
            services.AddSignalR().AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddHostedService<EquipoBiometricoWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<EquipoBiometricoHub>("/hubs/equipos-biometricos");
            });
        }
    }
}
