using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Services;
using GitHubClientLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DentsuAegis
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.Configure<GitHubClientOptions>(Configuration.GetSection(nameof(GitHubClientOptions)));
            //services.AddOptions<GitHubClientOptions>(nameof(GitHubClientOptions));
            services.AddSingleton<IGitHubClient, GitHubClient>();
            services.AddScoped<IRepositoryCrudService, RepositoryCrudService>();
            services.AddHttpClient();

            services.AddDbContext<DataContext>(o => o.UseNpgsql(Configuration.GetConnectionString(nameof(DataContext))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(builder =>
           builder.WithOrigins("http://localhost:8080")
             .AllowAnyHeader()
             .AllowAnyMethod()
             .AllowCredentials());
        }
    }
}
