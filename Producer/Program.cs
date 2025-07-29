using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() 
{
HostName = "localhost",
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

Console.WriteLine("What would you like to send? ");
string? message = Console.ReadLine();
var body = Encoding.UTF8.GetBytes(message?? "Default msg");

channel.BasicPublish
    (
    exchange: "",
    routingKey: "ayodhya-queue",
    basicProperties: null,
    body: body
    );

Console.WriteLine($"[Ayodhya] Sent: {message}");
