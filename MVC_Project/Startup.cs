using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_Project.Data;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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
            services.AddMvc();
            services.AddDbContext<StoreDataContext>(options =>
            {
                options.UseSqlite("DataSource=storeData.db");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env , StoreDataContext dataContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
            SeedDataBase(dataContext);

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();
        }
        private void SeedDataBase(StoreDataContext context)
        {
            var anonymousUser = new User()
            {
                FirstName = "anonymous",
                LastName = "anonymous",
                BirthDate = DateTime.Now,
                Email = "anonymous@gmail.com",
                Username = "anonymous",
                PasswordHash = "0000",
                Id = 1
            };
            var test_user_1 = new User()
            {
                FirstName = "Pepe",
                LastName = "Kek",
                BirthDate = DateTime.Now,
                Email = "pepe_keke@gmail.com",
                Username = "Frog",
                PasswordHash = "1234",
                Id = 2
            };
            var test_user_2 = new User()
            {
                FirstName = "Pepe",
                LastName = "Kek",
                BirthDate = DateTime.Now,
                Email = "pepe_keke@gmail.com",
                Username = "Frog",
                PasswordHash = "1234",
                Id = 3
            };
            var test_prod_1 = new Product()
            {
                Id = 0,
                Date = DateTime.Now,
                LongDescription = "jack lad schooner scallywag dance the hempen jig carouser broadside cable strike colors. Bring a spring upon her cable holystone blow the man down spanker Shiver me timbers to go on account lookout wherry doubloon chase. Belay yo-ho-ho keelhaul squiffy black spot yardarm spyglass sheet transom heave to.",
                ShortDescription = "Trysail Sail ho Corsair red ensign hulk smartly",
                SellerId = 1,
                BuyerId = 2,
                Price = 100.456,
                State = Product.States.Available,
                Title = "Spoon"
            };
            context.Users.Add(anonymousUser);
            context.SaveChanges();
            context.Users.Add(test_user_1);
            context.SaveChanges();
            context.Users.Add(test_user_2);
            context.SaveChanges();
            context.Products.Add(test_prod_1);
            context.SaveChanges();
        }

    }
}
