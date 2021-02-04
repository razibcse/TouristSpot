using BLL.Interface;
using BLL.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repositories.Interface;
using Repositories.Repo;
using Repository.Interface;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TouristSpot.Data;
using TouristSpot.UserServices;

namespace TouristSpot
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders().AddDefaultUI();

            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddTransient<ILikeManager, LikeManager>();
            services.AddTransient<ILikeRepository, LikeRepository>();

            services.AddTransient<IRatingManager, RatingManager>();
            services.AddTransient<IRatingRepository, RatingRepository>();

            services.AddTransient<ICommentManager, CommentManager>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IImageManager, ImageManager>();
            services.AddTransient<IImageRepository, ImageRepository>();

            services.AddTransient<IVideoManager, VideoManager>();
            services.AddTransient<IVideoRepository, VideoRepository>();


            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Posts}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
