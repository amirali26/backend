using System;
using System.Net;
using Api.Database.MySql;
using dashboard.Accounts;
using dashboard.AccountUserInvitations;
using dashboard.AreasOfPractices;
using dashboard.Enquiries;
using dashboard.Requests;
using dashboard.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddHealthChecks();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var region = Configuration["CognitoConfiguration:Region"];
                    var userPoolId = Configuration["CognitoConfiguration:UserPoolId"];
                    var appClientId = Configuration["CognitoConfiguration:AppClientId"];
                    options.TokenValidationParameters = new TokenValidationParameters {ValidateAudience = false};
                    options.Authority = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
                    options.RequireHttpsMetadata = false;
                });
            services.AddAuthorization();
            services
                .AddCors(options =>
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3001",
                                    "http://localhost:3002",
                                    "http://localhost:3000",
                                    "https://solicitor.helpmycase.co.uk",
                                    "https://forms.helpmycase.co.uk")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    )
                )
                .AddDbContext<DashboardContext>(
                    options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                )
                .AddScoped<IAccountUserInvitationService, AccountUserInvitationService>()
                .AddGraphQLServer()
                .AddProjections()
                .AddHttpRequestInterceptor<HttpRequestInterceptor>()
                .AddAuthorization()
                .AddQueryType(d => d.Name("Query"))
                .AddType<AreasOfPracticeQueries>()
                .AddType<AccountQueries>()
                .AddType<RequestQueries>()
                .AddType<UserQueries>()
                .AddType<EnquiryQueries>()
                .AddType<AccountUserInvitationQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddType<AccountMutations>()
                .AddType<UserMutations>()
                .AddType<RequestMutations>()
                .AddType<EnquiryMutations>()
                .AddType<AccountUserInvitationMutations>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapGraphQL();
            });
        }
    }
}