using BLL.Interfaces;
using BLL.Repostioes;
using DAL.ContextConfiguration;
using DAL.Moduls;
using DemoMvc.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Dbcontext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("defaltConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UniteOfWork>();
            builder.Services.AddAutoMapper(e => e.AddProfile(new ClientProfile()));
            builder.Services.AddAutoMapper(e => e.AddProfile(new GalleryProfile()));
            builder.Services.AddAutoMapper(e => e.AddProfile(new HairArtistProfile()));

            var app=builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });

            app.Run();
        }

       
    }
}
