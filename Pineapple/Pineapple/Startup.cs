using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pineapple.Services;
using Pineapple.Controllers;
using log4net;
using System.Reflection;
using log4net.Config;

namespace Pineapple
{
    public class Startup
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Startup(IHostingEnvironment env)
        {
            log4net.Config.XmlConfigurator.Configure();
            
            LogUsing4net _Log = new LogUsing4net(); 
            _Log.WriteWarning("Some text");
            _Log.WriteInfo("Some text");
            _Log.WriteFatal("Some text");
            _Log.WriteDebug("Some text");
            _Log.WriteError("Some text");
             _Log.WriteDebugExp("Some text",new Exception("Exception"));


            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddTransient<IUserAuth, UserAuth>();
            services.AddTransient<ILogG, LogUsing4net>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
