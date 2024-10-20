using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IntegrationApi.Data.RabitMQServices
{
    public class RabitMQService : IRabitMQService
    {
        private readonly IConfiguration _configuration;

        public RabitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMessage<T>(T message, string QueueName)
        {
            string HostName = _configuration.GetValue<string>("KeyList:rabbitmqUrl");
            string PortNo = _configuration.GetValue<string>("KeyList:rabbitmqPort");
            var factory = new ConnectionFactory { HostName = HostName, Port = Convert.ToInt32(PortNo) };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(QueueName, exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "", routingKey: QueueName, body: body);
        }

        public string ReceiveMessages(string QueueName)
        {
            string HostName = _configuration.GetValue<string>("KeyList:rabbitmqUrl");

            string responce = "";
            var factory = new ConnectionFactory() { HostName = HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received message: {message}");
                    responce = message;
                };
                channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);


            }
            return responce;
        }
    }
}
