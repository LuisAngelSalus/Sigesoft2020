﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SL.Sigesoft.Data;
using SL.Sigesoft.Data.Contracts;
using SL.Sigesoft.Data.Contracts.Win;
using SL.Sigesoft.Data.Repositories;
using SL.Sigesoft.Data.Repositories.Win;
using SL.Sigesoft.Models;
using SL.Sigesoft.WebApi.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SL.Sigesoft.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            _configuration = configuration;
        }
        public IConfiguration _configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Sigesoft API",
                    Version = "v1.1",
                    Description = "Simple RESTful API built with ASP.NET Core 2.2 to show how to create RESTful services using a decoupled, maintainable architecture.",
                    Contact = new Contact
                    {
                        Name = "Luis",
                        Url = "https://github.com/LuisAngelSalus/",
                    },
                    License = new License
                    {
                        Name = "MIT",
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<SigesoftCoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("SigesoftCoreDB")));
            services.AddDbContext<SigesoftWinContext>(options => options.UseSqlServer(_configuration.GetConnectionString("SigesoftWinDB")));
            services.AddScoped<IPersonsRepository, PersonRepository>();
            services.AddScoped<ISystemUserRepository, SystemUserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyHeadquarterRepository, CompanyHeadquarterRepository>();
            services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
            services.AddScoped<IInfoRepository, InfoRepository>();
            services.AddScoped<IInterfaceSigesoftWinRepository, ComponentRepository>();
            services.AddScoped<IProtocolProfileRepository, ProtocolProfileRepository>();
            services.AddScoped<IQuotationRepository, QuotationRepository>();
            services.AddScoped<ISecuentialRespository, SecuentialRespository>();
            services.AddScoped<IQuoteTrackingRepository, QuoteTrackingRepository>();
            services.AddScoped<IPriceListRepository, PriceListRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IOwnerCompanyRepository, OwnerCompanyRepository>();
            services.AddScoped<IProtocolRepository, ProtocolRepository>();
            services.AddScoped<IProtocolDetailRepository, ProtocolDetailRepository>();
            services.AddScoped<ISubscriptionRepository, SuscriptionRespository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IAccountSettingRepository, AccountSettingRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IClientUserRepository, ClientUserRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IPasswordHasher<SystemUser>, PasswordHasher<SystemUser>>();
            services.AddScoped<IPasswordHasher<ClientUser>, PasswordHasher<ClientUser>>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<TokenService>();

            //Accedemos a la sección JwtSettings del archivo appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            //Obtenemos la clave secreta guardada en JwtSettings:SecretKey
            string secretKey = jwtSettings.GetValue<string>("SecretKey");
            //Obtenemos el tiempo de vida en minutos del Jwt guardada en JwtSettings:MinutesToExpiration
            int minutes = jwtSettings.GetValue<int>("MinutesToExpiration");
            //Obtenemos el valor del emisor del token en JwtSettings:Issuer
            string issuer = jwtSettings.GetValue<string>("Issuer");
            //Obtenemos el valor de la audiencia a la que está destinado el Jwt en JwtSettings:Audience
            string audience = jwtSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(minutes)
                };
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sigesoft API");
            });

            app.UseMvc();
        }
    }
}
