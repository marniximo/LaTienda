using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.Clientes.AFIP;
using LaTienda.Models;
using System.Threading;
using LaTienda.Repository.Interfaces;
using LaTienda.Repository;
using Microsoft.EntityFrameworkCore.Design;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

namespace LaTienda
{
    public class Startup
    {
        public static IConfiguration StaticConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration;

            

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Allow CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            //JWT Authentication
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.LoginPath = "/Login/Login";
                options.AccessDeniedPath = "/Login/Login";
            });


            //DbContext
            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("AppConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                });
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //DI
            services.AddScoped<IClienteAfip, ClienteAfip>();
            services.AddScoped<ILoginTicketRepository, LoginTicketRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
            services.AddScoped<ILineaStockRepository, LineaStockRepository>();
            services.AddScoped<ILineaVentaRepository, LineaVentaRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ISucursalRepository, SucursalRepository>();
            services.AddScoped<ITalleRepository, TalleRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IClienteAfip clienteAfip)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //// Run Migrations DBs
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Console.WriteLine("Appling Migrations...");
                try
                {
                    var appDbContext = serviceScope.ServiceProvider.GetService<Context>();
                    var attempts = 3;
                    while (!appDbContext.Database.CanConnect() && attempts < 3)
                    {
                        Console.WriteLine("Waiting DbContext...");
                        Thread.Sleep(5000);
                    }
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("|---FAIL---| Appling Migrations" + ex.Message);
                }
            }

            app.UseStatusCodePages(async context => {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                // you may also check requests path to do this only for specific methods       
                // && request.Path.Value.StartsWith("/specificPath")
                {
                    response.Redirect("/Login/Login");
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
