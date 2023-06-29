using AltaMedia.Model;
using AltaMedia.Service.AdminBusiness;
using AltaMedia.Service.EventBusiness;
using AltaMedia.Service.NewsBusiness;
using AltaMedia.Service.TicketBusiness;
using AltaMedia.Service.UserBusiness;
using AltaMedia.Service.UserTicketBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AltaMedia
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
            services.AddMvc()
            .AddSessionStateTempDataProvider();

            services.AddSession();

            services.AddCors();

            services.AddControllers();

            // Dependency Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEventBusiness, EventBusiness>();
            services.AddTransient<ITicketBusiness, TicketBusiness>();
            services.AddTransient<IUserTicketBusiness, UserTicketBusiness>();
            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<INewsBusiness, NewsBusiness>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hue Festival", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<AltaMediaDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("DBConnection"));
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // add authentication
            //app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alta Media v1");
            });

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
