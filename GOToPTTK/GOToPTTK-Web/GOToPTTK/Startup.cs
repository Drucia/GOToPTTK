using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Community.AspNetCore.ExceptionHandling;
using System.Data.SqlClient;

namespace GOToPTTK
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddSingleton<IRouteListService, RouteListService>();
            services.AddSingleton<IPlaceListService, PlaceListService>();
            services.AddSingleton<IGuideService, GuideService>();
            services.AddSingleton<ITripListService, TripListService>();
            services.AddSingleton<IVerifyService, VerifyService>();
            services.AddScoped<IApiCustomRouteService, ApiCustomRouteService>();
            services.AddScoped<ITripService, TripService>();

            services.AddMvc(options => {
                options.Filters.Add(new SqlExceptionFilter());
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => $"Wprowadzono błędne dane.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => $"Pole musi być liczbą.");
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => $"Pole jest wymagane.");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"Wprowadzono błędne dane.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Pole jest wymagane");
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => $"Wprowadzono błędne dane.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Pole jest wymagane.");
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }*/

            app.UseStatusCodePages();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
