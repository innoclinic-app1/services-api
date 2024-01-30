namespace WebApp;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var webHost = CreateHostBuilder(args).Build();
            await webHost.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
