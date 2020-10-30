using FluentValidation.AspNetCore;
using AspNET.Models;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNET.Models.Services.Invoices;
using AspNET.Models.Services.Email;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AspNET
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
            services.AddCors(opt =>
                 {
                     opt.AddPolicy("AppPolicy", builder =>
                     {
                         builder
                             .WithOrigins("http://localhost:3000")
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .WithExposedHeaders("Content-Disposition", "x-suggested-filename");
                     });
                 });

            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddAutoMapper(typeof(Startup));

            services.Configure<ConnectionStringsOptions>(Configuration
                .GetSection("ConnectionString"));
            services.Configure<InvoicesOptions>(Configuration
                .GetSection("Invoices"));
            services.Configure<AuthOptions>(Configuration
                .GetSection("Auth"));
            services.Configure<SmtpOptions>(Configuration
                .GetSection("SmtpOptions"));

            IConfigurationSection connectionSection = Configuration.GetSection("ConnectionStrings");
            var connectionStringOptions = new ConnectionStringsOptions();
            connectionSection.Bind(connectionStringOptions);

            IConfigurationSection authSection = Configuration.GetSection("Auth");
            var authOptions = new AuthOptions();
            authSection.Bind(authOptions);

            services.AddDbContextPool<InvoiceDbContext>(opt => opt
                .UseSqlServer(connectionStringOptions.DefaultConnection));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<InvoiceDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;

                var secretByte = Encoding.UTF8.GetBytes(authOptions.Secret);
                var key = new SymmetricSecurityKey(secretByte);
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(1);
            });

            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AppPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
