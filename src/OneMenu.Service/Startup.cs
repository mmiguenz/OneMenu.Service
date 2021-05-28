using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using OneMenu.Core.actions;
using OneMenu.Core.Repositories;
using OneMenu.Data.AutoMapper;
using OneMenu.Data.Repositories;

namespace OneMenu.Service
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "OneMenu.Service", Version = "v1"});
            });
            
            services.AddScoped<ListMenus, ListMenus>();
            var mongoClient = GetMongoClient();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.Add(new ServiceDescriptor(typeof(MongoClient), (_) => GetMongoClient(), ServiceLifetime.Scoped));
            
            services.AddAutoMapper(typeof(MenuProfile));
        }

        private MongoClient GetMongoClient()
        {
          return  new MongoClient(
                "mongodb+srv://mmiguenz-utn:BLtI2y6dY3gMWBxf@cluster0.2gopv.mongodb.net/onemenu?retryWrites=true&w=majority");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneMenu.Service v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}