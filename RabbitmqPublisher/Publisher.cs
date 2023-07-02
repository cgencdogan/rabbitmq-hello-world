using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
string message;
byte[] body;

message = Console.ReadLine();

while (message!="stop")
{
    body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: body);
    Console.WriteLine($" [x] Sent {message}");
    Console.WriteLine(" Type [stop] to stop program.");
    message = Console.ReadLine();
}