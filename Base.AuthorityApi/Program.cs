using Base.Util.Core8.Customs;

namespace Base.AuthorityApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            services.AddCustomCore();

            var app = builder.Build();
            app.UseCustomCore();
            app.Run();
        }
    }
}
