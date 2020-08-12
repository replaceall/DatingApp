using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using DatingApp.API.Helper;
using AutoMapper;
using Microsoft.Extensions.Hosting;

namespace DatingApp.API
{
    public class Startup
    {
       // readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>( x => {
            //    x.UseLazyLoadingProxies();
            //    x.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"));
            //});
            //ConfigureServices(services);

            // For Sqlite
            //services.AddDbContext<DataContext>(x => {
            //    x.UseLazyLoadingProxies();
            //    x.UseSqlite(Configuration.GetConnectionString("DefaultConnectionString"));
            //});
            //ConfigureServices(services);

            //For SqlServer
            services.AddDbContext<DataContext>(x =>
            {
                x.UseLazyLoadingProxies();
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });
            ConfigureServices(services);
        }


        public void ConfigureProductionServices(IServiceCollection services)
        {
            //services.AddDbContext<DataContext>(x => {
            //    x.UseLazyLoadingProxies();
            //    x.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"));
            //});
            //ConfigureServices(services);
            services.AddDbContext<DataContext>(x => {
                x.UseLazyLoadingProxies();
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            });
            ConfigureServices(services);

           // for SQLITE

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnectionString")));
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("http://localhost:4200/")
            //                                              .AllowAnyHeader()
            //                                              .AllowAnyMethod();
            //                      });
            //});
            services.AddControllers()
                .AddNewtonsoftJson(options=> {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            //    .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 
            //services.AddCors();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(DatingRepository).Assembly);
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IDatingRepository, DatingRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddScoped<UserActivityLog>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                //app.UseExceptionHandler(buider =>
                //{
                //    buider.Run(async context =>
                //    {
                //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //        var error = context.Features.Get<IExceptionHandlerFeature>();
                //        if (error != null)
                //        {
                //            context.Response.AddApplicationError(error.Error.Message);
                //            await context.Response.WriteAsync(error.Error.Message);
                //        }
                //    });
                //});
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors( x=> x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endPoints =>
               {
                   endPoints.MapControllers();
                   endPoints.MapFallbackToController("Index", "FallBack");
               });
           // app.UseMvc();
        }
    }
}
