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
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace MVC_Project
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        { Configuration = configuration; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<StoreDataContext>(options =>
            { options.UseSqlite("DataSource=storeData.db"); });

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<StoreDataContext>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StoreDataContext dataContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
            SeedDataBase(dataContext, env);

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        private void SeedDataBase(StoreDataContext context, IHostingEnvironment env)
        {
            var test_image = new ProductImage()
            {
                ByteArray = File.ReadAllBytes($"{env.ContentRootPath}\\wwwroot\\images\\test_image_1.jpg"),
                ProductId = 1,
                Id = 1,
                FileName = "test_image_1.jpg",
            };
            context.ProductImages.Add(test_image);

            var test_user_1 = new User()
            {
                //UserId = 1,
                Id = Guid.NewGuid().ToString(),
                FirstName = "anonymous",
                LastName = "anonymous",
                BirthDate = DateTime.Now,
                Email = "anonymous@gmail.com",
                UserName = "anonymous",
            };
            var test_user_2 = new User()
            {
                //UserId = 2,
                Id = Guid.NewGuid().ToString(),
                FirstName = "Pepe",
                LastName = "Kek",
                BirthDate = DateTime.Now,
                Email = "pepe_keke@gmail.com",
                UserName = "Frog",
            };
            var test_user_3 = new User()
            {
                //UserId = 3,
                Id = Guid.NewGuid().ToString(),
                FirstName = "Samwise",
                LastName = "Gamgee",
                BirthDate = DateTime.Now,
                Email = "bigMistake@gmail.com",
                UserName = "garden122",
            };
            context.Users.Add(test_user_1);
            context.Users.Add(test_user_2);
            context.Users.Add(test_user_3);

            var test_prod_1 = new Product()
            {
                Id = 1,
                Date = DateTime.Now,
                LongDescription = "jack lad schooner scallywag dance the hempen jig carouser broadside cable strike colors. Bring a spring upon her cable holystone blow the man down spanker Shiver me timbers to go on account lookout wherry doubloon chase. Belay yo-ho-ho keelhaul squiffy black spot yardarm spyglass sheet transom heave to.",
                ShortDescription = "Trysail Sail ho Corsair red ensign hulk smartly",
                SellerId = test_user_2.Id,
                BuyerId = test_user_3.Id,
                Price = 100.456,
                State = Product.States.Available,
                Title = "Spoon",
            };
            var test_prod_2 = new Product()
            {
                Id = 2,
                Date = DateTime.Now,
                LongDescription = "l o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o n g description.",
                ShortDescription = "shrt desc.",
                SellerId = test_user_3.Id,
                BuyerId = test_user_2.Id,
                Price = 420.69,
                State = Product.States.Available,
                Title = "A pile of shit",
            };
            var test_prod_3 = new Product()
            {
                Id = 3,
                Date = DateTime.Now,
                LongDescription = "l o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o n g description.",
                ShortDescription = "Trysail Sail ho Corsair red ensign hulk smartly",
                SellerId = test_user_1.Id,
                BuyerId = test_user_1.Id,
                Price = 1337.322,
                State = Product.States.Available,
                Title = "A bent spoon",
            };
            context.Products.Add(test_prod_1);
            context.Products.Add(test_prod_2);
            context.Products.Add(test_prod_3);

            context.SaveChanges();
        }
    }
}
