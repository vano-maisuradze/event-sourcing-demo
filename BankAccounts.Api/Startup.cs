using System;
using System.Linq;
using System.Reflection;
using App.Core;
using BankAccounts.Commands.Processor;
using BankAccounts.Commands.Repository;
using BankAccounts.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccounts.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IRepository, EventStoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            InitializeCommandProcessor(app.ApplicationServices);
        }

        private static void InitializeCommandProcessor(IServiceProvider serviceProvider)
        {
            var domain = typeof(CommandProcessor).GetTypeInfo().Assembly;
            var commands = domain.GetTypes()
                .Where(x =>
                    x.GetInterfaces().Any(y =>
                        y.GetTypeInfo().IsGenericType &&
                        y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));
            CommandProcessor.Initialize(serviceProvider, commands);
        }
    }
}
