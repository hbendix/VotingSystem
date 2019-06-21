using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VotingSystemRepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using VotingSystemServices;
using VotingSystemServices.Interfaces;
using VotingSystemRepositoryAccess.Interfaces;
using VotingSystemApi.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VotingSystemEntities.Enums;
using VotingSystemApi.Hub;

[assembly: ApiController]
namespace VotingSystemApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
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

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));

            services.AddSignalR();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserServices>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetUserById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }

                        if (context.HttpContext.Request.Path.ToString().Contains("Admin"))
                        {
                            if (user.Role != RoleType.Admin)
                            {
                                context.Fail("Unauthorized");
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAdminServices, AdminServices>();
            services.AddScoped<IAreaServices, AreaServices>();
            services.AddScoped<ICandidateServices, CandidateServices>();
            services.AddScoped<ICountingServices, CountingServices>();
            services.AddScoped<IElectionServices, ElectionServices>();
            services.AddScoped<IPartyServices, PartyServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IRegistrationServices, RegistrationServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IVotingServices, VotingServices>();

            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IElectionRepository, ElectionRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "CSSD API",
                    Description = "CSSD API",
                    TermsOfService = "None",
                });
            });

            // Register dependencies
            services.AddDbContext<VotingDbContext>(options 
                => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddDebug(builder);
            //loggerFactory.AddDebug();

            env.EnvironmentName = EnvironmentName.Development;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            
            app.UseStatusCodePages();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<VoteHub>("/stream");
            });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
