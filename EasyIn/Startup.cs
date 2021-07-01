using EasyIn.Domain;
using EasyIn.Models;
using EasyIn.Repositories;
using EasyIn.Repositories.Contexts;
using EasyIn.Repositories.Interfaces;
using EasyIn.Services;
using EasyIn.Services.Models;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyIn
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Env { get; }
        private readonly string CorsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(Options =>
            {
                Options.AddPolicy(
                    CorsPolicy,
                    builder => builder
                    //.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    );
            });

            services.AddControllers();
            services.AddHttpClient();

            services.AddDbContext<MyContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    );

            services.AddScoped<MyContext>();

            //Repositories
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<ICredentialRepository, CredentialRepository>();
            services.AddScoped<IQrCodeLoginRepository, QrCodeLoginRepository>();

            //Services
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<EmailOptions>(options =>
            {
                options.HostAddress = Configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.HostPort = Convert.ToInt32(Configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.HostUsername = Configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.HostPassword = Configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.SenderName = Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            //Authentication
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
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

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EasyIn API",
                    Version = "v1",
                    Description = "",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: '12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var result = JsonConvert.SerializeObject(
                    //new ResponseError("Erro não tratado. Entre em contato com o administrador do sistema"),
                    new ResponseError(exception.Message + exception.InnerException != null ? " - " + exception.InnerException.Message : ""),
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("../swagger/v1/swagger.json", "EasyIn"));
        }
    }
}
