using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Bank;
using BankApi.AccountOwner;
using BankApi.BankCustomer;
using BankApi.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BankApi
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddScoped<BankCustomerDbContext>();
            services.AddScoped<AccountOwnerDbContext>();
            services.AddScoped(sp => CreateRolesFromClaims(sp).Single(r => r is Bank.BankCustomer) as Bank.BankCustomer);
            services.AddScoped(sp => CreateRolesFromClaims(sp).Single(r => r is Bank.AccountOwner) as Bank.AccountOwner);
            services.AddDbContext<Database>(builder => builder.UseInMemoryDatabase("AfterHours"));
            
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "BankApi", Version = "v1"}); });
        }

        private static IEnumerable<Role> CreateRolesFromClaims(IServiceProvider sp)
        {
            var httpContextAccessor = sp.GetService<IHttpContextAccessor>();
            var user = httpContextAccessor?.HttpContext?.User;
            var claims = user?.Claims ?? Enumerable.Empty<Claim>();
            return
                claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => Role.CreateFromString(c.Value, user?.Identity?.Name));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApi v1"));
            }

            app.UseRouting();
            app.UseCors(p => p
                .WithOrigins("http://localhost:4000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}