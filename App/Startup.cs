using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Salinger.Core.Applications;
using Salinger.Core.Applications.Actions;
using Salinger.Core.Domains;
using Salinger.Core.Domains.Actions;
using Salinger.Core.Infrastructures;
using Salinger.Core.Infrastructures.Exchanges;
using Salinger.Core.Infrastructures.Databases;

namespace Salinger.Core
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
            services.AddDbContext<SalingerDbSqliteDbContext>(options =>
                options.UseSqlite(this.Configuration.GetConnectionString("SalingerDb")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Salinger.Core", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Salinger.Core v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            int millisecondsDelay = Configuration.GetSection("SalingerScheduler").GetValue<int>("MillisecondsDelay");
            SalingerSearcher seacher = new SalingerSearcher();
            SalingerScheduler scheduler = new SalingerScheduler(2);
            List<Task> tasks = new List<Task>();
            TaskFactory factory = new TaskFactory(scheduler);
            CancellationTokenSource ctSource = new CancellationTokenSource();
            string uri = this.Configuration.GetSection("EWS").GetValue<string>("URI");
            int maxCountOfItems = this.Configuration.GetSection("EWS").GetValue<int>("MaxCountOfItems");
            string account = this.Configuration.GetSection("Salinger").GetValue<string>("AccountName");
            string password = this.Configuration.GetSection("Salinger").GetValue<string>("AccountPassword");
            ExchangeWebServiceContext ewsContext = new ExchangeWebServiceContext(
                uri, 
                maxCountOfItems,
                account,
                password
            );
            string dbConnectionString = this.Configuration.GetConnectionString("SalingerDb");
            SalingerDataAccessAdapter adapter = new SalingerDataAccessAdapter(ewsContext, dbConnectionString);
            string mailAddress = this.Configuration.GetSection("Salinger").GetValue<string>("EmailAddress"); 
            SearchActionManager manager = new SearchActionManager(adapter, mailAddress);
            IPollingSearchAction polling = new PollingSearchAction(millisecondsDelay, seacher, manager);
            Task task = factory.StartNew(() => {
                polling.Run();
            });
            // todo how to ctSource.Dispose()...
            // ctSource.Dispose();
        }
    }
}
