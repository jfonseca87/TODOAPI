using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TODOBusiness.Class;
using TODOBusiness.Interfaces;
using TODORepository.Class;
using TODORepository.Interfaces;

namespace TODOAPI
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TODOContext>(options =>
               options.UseSqlServer(this.configuration["ConnectionStrings:TODOContext"])
            );

            //Repository Container
            services.AddTransient<ITodoRepository, TodoRepository>();

            //Business Container
            services.AddTransient<ITodoBusiness, TodoBusiness>();

            services.AddMvc();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TODO API",
                    Description = "API for TODO maintenance",
                    Contact = new Contact
                    {
                        Name = "Jorge Fonseca",
                        Email = "jorge.fonseca87@gmail.com"
                    }
                });
            });

            services.AddCors(cors =>
                cors.AddPolicy("TODOCorsPolicy", build =>
                build.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin()
                     .AllowCredentials()
            ));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO API v1");
            });
            app.UseCors("TODOCorsPolicy");
        }
    }
}
