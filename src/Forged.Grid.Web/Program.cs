using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Forged.Grid.Web
{
    public class Program
    {
        public static void Main()
        {
            new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseKestrel()
                .UseIIS()
                .Build()
                .Run();
        }
    }
}
