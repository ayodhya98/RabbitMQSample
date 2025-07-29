using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare
    (
    queue: "ayodhya-queue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
    );

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[Consumer] Received: {message}");
};

channel.BasicConsume
    (
    queue: "ayodhya-queue",
    autoAck: true,
    consumer: consumer
    );

Console.WriteLine("Waiting for messages.....");
Console.ReadLine();