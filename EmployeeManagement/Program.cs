using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement
{
    public class Program
    {
        public class ConfigurationManager
        {
            private readonly IConfiguration _config;
            public ConfigurationManager(IConfiguration config)
            {
                _config = config;
            }

            public static string? getConnectionString (IConfiguration config)
            {
                string? value = config.GetConnectionString("EmployeeDBConnection");
                return value;
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(ConfigurationManager.getConnectionString(builder.Configuration)));
            builder.Services.AddControllersWithViews();
            builder.Services.AddRouting();
            builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            var app = builder.Build();

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.MapGet("/", () => TestModel.getValue(app.Configuration));
            //app.UseFileServer(fileServerOptions);
        
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });


            app.Run();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello world");
            //});
        }
    }
}