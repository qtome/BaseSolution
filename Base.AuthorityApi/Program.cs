namespace Base.AuthorityApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            var app = builder.Build();

            app.Run();
        }
    }
}
