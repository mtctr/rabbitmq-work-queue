using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "task_queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
    );

string message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

channel.BasicPublish(
    exchange: "",
    routingKey: "task_queue",
    basicProperties: properties,
    body: body
    );

Console.WriteLine($" [x] Sent {message}");


string GetMessage(string[] args)
{
    if (args.Length > 0)
        return string.Join(" ", args);

    return "Hello World!";
}