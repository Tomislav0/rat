using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Portfolio.DAL;
using Portfolio.DAL.Models.Account;
using Portfolio.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Portfolio.WebAPI.JwtFeatures;

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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                                  });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<JwtHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<PortfolioDBConnectionSetting>(Configuration.GetSection("ConnectionStrings"));
            services.AddDbContextPool<PortfolioDB>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Portfolio.DAL")
                         .EnableRetryOnFailure(maxRetryCount: 3))
                .UseLazyLoadingProxies();
            });

            services.AddTransient<PortfolioDBContextFactory, PortfolioDBContextFactory>();


            services.AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<PortfolioDB>();

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

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
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
