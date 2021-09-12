using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WhoWantsToBeAMillionaire.Models;
using WhoWantsToBeAMillionaire.Models.Repositories;
using WhoWantsToBeAMillionaire.Models.Services;

namespace WhoWantsToBeAMillionaire
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration config) => Configuration = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MillionaireDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WhoWantsToBeAMillionaireConnection"));
            }, ServiceLifetime.Singleton);
            services.AddSingleton<IQuestionRepository, QuestionRepository>();
            services.AddSingleton<IAnswerRepository, AnswerRepository>();
            services.AddSingleton<GameService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
