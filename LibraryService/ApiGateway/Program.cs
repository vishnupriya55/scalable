namespace ApiGateway
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Calls CreateHostBuilder function, which will return IHostBuilder instance. 
            //To run the application, we should build the instance returned using Build() and to run application use Run().
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //CreateDefaultBuilder initializes and returns HostBuilder class with default configuration
            //ConfigureWebHostDefaults class will accept HostBuilder's instance and configure parameter => which is Startup class
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("configuration.json");
            });
    }
}