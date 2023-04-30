using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public class Program
    {
        public class TestModel
        {
            private readonly IConfiguration _config;
            public TestModel(IConfiguration config)
            {
                _config = config;
            }

            public static string getValue (IConfiguration config)
            {
                string value = config["MyKey"];
                return value;
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddRouting();
            builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
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