using Confluent.Kafka;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Helper
{
    public class Helper
    {
        public static ProducerConfig GetProducerConfig()
        {
            return new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Kafka broker(s) address
            };
        }
        public async Task producer(RegistrationDetailsForPublishing res)
        {
           
            // Serialize registration details to a JSON string
            var registrationDetailsJson = Newtonsoft.Json.JsonConvert.SerializeObject(res);

            // Get Kafka producer configuration
            var producerConfig = GetProducerConfig();

            // Create a Kafka producer
            using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
            {
                try
                {
                    // Publish registration details to Kafka topic
                    await producer.ProduceAsync("Registration-topic", new Message<Null, string> { Value = registrationDetailsJson });
                    Console.WriteLine("Registration details published to Kafka topic.");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Failed to publish registration details to Kafka topic: {e.Error.Reason}");
                }
            }
        }
    }
}
