﻿using System;
using INTEX.Areas.Identity.Data;
using INTEX.Areas.Identity.Services;
using INTEX.Data;
using INTEX.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(INTEX.Areas.Identity.IdentityHostingStartup))]
namespace INTEX.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<INTEXDbContext>(options =>
                    options.UseMySql(DbHelper.GetRDSConnectionString("Identity")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<INTEXDbContext>();

                services.AddTransient<IEmailSender, SendGridEmailSender>();
            });
        }
    }
}