using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProducerWorker.Infrastructure.Messaging;

namespace ProducerWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddOptions<ProducerWorkerOptions>()
                        .Bind(hostContext.Configuration);

                    services.AddTransient<IKafkaProducerBuilder, KafkaProducerBuilder>();

                    services.AddTransient<IMessageProducer, KafkaMessageProducer>();
                });
        }
    }
}