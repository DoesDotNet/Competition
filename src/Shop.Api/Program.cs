namespace Shop.Api;

public static class Program
{
    static async Task Main()
    {
        await CreateHostBuilder()
            .Build()
            .RunAsync();
    }

    internal static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseStartup<Startup>();
            });
        // .ConfigureLogging(logging =>
        // {
        //     logging.AddConsole();
        // });
    }
}