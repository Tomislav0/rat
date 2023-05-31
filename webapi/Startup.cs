using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portfolio.DAL;
using Portfolio.DAL.Models.Account;

namespace Portfolio.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<PortfolioDBConnectionSetting>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContextPool<PortfolioDB>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Portfolio.DAL")
                         .EnableRetryOnFailure(maxRetryCount: 3))
                .UseLazyLoadingProxies();
            });

            services.AddTransient<PortfolioDBContextFactory,PortfolioDBContextFactory>();

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<PortfolioDB>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<PortfolioDB>();
            services.AddControllers();

        }

       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
