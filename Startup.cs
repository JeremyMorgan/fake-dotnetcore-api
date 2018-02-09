using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using fake_dotnetcore_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fake_dotnetcore_api
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
            services.AddDbContext<ApiContext>(optionsAction: opt => opt.UseInMemoryDatabase("FakeAPI"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);
            app.UseMvc();
        }
        private static void AddTestData(ApiContext context)
        {
            context.Movies.Add(new Movie() {Title = "Dumb and Dumber", Description = "The cross-country adventures of two good-hearted but incredibly stupid friends.", Year = 1994, Rating = "PG-13"});
            context.Movies.Add(new Movie() {Title = "One Crazy Summer", Description = "An aspiring teenage cartoonist and his friends come to the aid of a singer trying to save her family property from developers.", Year = 1986, Rating = "PG"});
            context.Movies.Add(new Movie() {Title = "Police Academy 4: Citizens on Patrol", Description = "The misfit Police Academy graduates now are assigned to train a group of civilian volunteers to fight crime once again plaguing the streets.", Year = 1987, Rating = "PG"});
            context.Movies.Add(new Movie() {Title = "Rocky 4", Description = "After iron man Drago a highly intimidating 6-foot-5 261-pound Soviet athlete kills Apollo Creed in an exhibition match Rocky comes to the heart of Russia for 15 pile-driving boxing rounds of revenge.", Year = 1985, Rating = "PG"});
            context.Movies.Add(new Movie() {Title = "Old School", Description = "Three friends attempt to recapture their glory days by opening up a fraternity near their alma mater.", Year = 2003, Rating = "R"});
            context.Movies.Add(new Movie() {Title = "Grease", Description = "Good girl Sandy and greaser Danny fell in love over the summer. When they unexpectedly discover they're now in the same high school will they be able to rekindle their romance?", Year = 1978, Rating = "PG-13"});
            context.Movies.Add(new Movie() {Title = "A Christmas Story", Description = "In the 1940s a young boy named Ralphie attempts to convince his parents his teacher and Santa that a Red Ryder BB gun really is the perfect Christmas gift.", Year = 1983, Rating = "PG"});
            context.Movies.Add(new Movie() {Title = "Joe Dirt", Description = "After being abandoned by his parents at the Grand Canyon Joe Dirt tells the story of his journey to find his parents.", Year = 2001, Rating = "PG-13"});
            context.Movies.Add(new Movie() {Title = "Super Troopers", Description = "Five Vermont state troopers avid pranksters with a knack for screwing up try to save their jobs and out-do the local police department by solving a crime.", Year = 2001, Rating = "R"});
            context.Movies.Add(new Movie() {Title = "Days of Thunder", Description = "A young hot-shot stock car driver gets his chance to compete at the top level.", Year = 1990, Rating = "PG-13"});
            context.SaveChanges();
        }
    }
}
