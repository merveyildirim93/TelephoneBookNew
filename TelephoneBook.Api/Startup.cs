using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TelephoneBook.Business.ContactInfoBusiness;
using TelephoneBook.Business.PersonBusiness;
using TelephoneBook.Business.ReportBusiness;
using TelephoneBook.DataAccess.Context;

namespace TelephoneBook.Api
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
            services.AddDbContext<PostreSqlContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TelephoneDirectory.DataAccess")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TelephoneBook.Api", Version = "v1" });
            });
            services.AddTransient<IPersonBusiness, PersonBusiness>();
            services.AddTransient<IContactInfoBusiness, ContactInfoBusiness>();
            services.AddTransient<IReportBusiness, ReportBusiness>();
            services.AddCors(options => options.AddPolicy("AllowOrigin", p =>
            {
                p.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TelephoneBook.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
