namespace CacheSurvy
{
    using System;
    using System.Threading;
    using CacheSurvy.DataAccess;
    using CacheSurvy.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(this.configuration);
            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettings>>();

            services.AddMemoryCache();

            services.AddDistributedRedisCache(options => options.Configuration = settings.Value.ConnectionStrings.Cache);
            services.AddScoped<IMongoDBHelper, MongoDBHelper>();

            services.AddDistributedMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IDataAccessService, DataAccessService>();
            services.AddSingleton<IRedisCacheHelper, RedisCacheHelper>();
            services.AddSingleton<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddSingleton<MemoryHelper>();
            services.AddSingleton<MyMemoryCache>();

            // TODO: 對時
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IOptions<AppSettings> settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();
            app.UseMvc();

            this.MakeThreadRunInBackground();
        }

        private void MakeThreadRunInBackground()
        {
            // This is because that it's asynchronous to return response and forward message,
            // so thread should run in background mode to avoid aborting when controller is done its job.
            Thread.CurrentThread.IsBackground = false;
        }
    }
}
