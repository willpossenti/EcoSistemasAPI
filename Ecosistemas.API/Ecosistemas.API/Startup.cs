﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using Ecosistemas.API.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


using static Ecosistemas.API.Security.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using Ecosistemas.API.Business;

namespace Ecosistemas.API
{
    public class Startup
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public Startup(IConfiguration configuration)
        {
            //Configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CatalogoDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<UserService>();
            services.AddScoped<AccessManager>();

            _signingConfigurations = new SigningConfigurations();
            services.AddSingleton(_signingConfigurations);

            _tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                        .Configure(_tokenConfigurations);
            services.AddSingleton(_tokenConfigurations);

            //Aciona a extensão que irá configurar o uso de
            //autenticação e autorização via tokens
            services.AddJwtSecurity(_signingConfigurations, _tokenConfigurations);

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CatalogoDbContext context, IServiceProvider services)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        new IdentityInitializer(context, _signingConfigurations, _tokenConfigurations, services)
           .Initialize();

        app.UseHttpsRedirection();
        app.UseMvc();
    }
}

    

}