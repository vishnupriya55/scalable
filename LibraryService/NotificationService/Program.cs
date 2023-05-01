using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;
using NotificationService;
using MongoDB.Driver;

class Program
{
    private IMongoCollection<NotificationDTO> _notificationCollection = null!;
    static void Main(string[] args)
    {
        const string QueueName = "Notification";
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Program instance = new();
            instance.CreateNotificationAsync(message);

        };
        channel.BasicConsume(queue: QueueName,
                             autoAck: true,
                             consumer: consumer);
        while (true)
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // You can add any additional cleanup code here

            // Exit the application
            return;
        }
    }

    private void CreateNotificationAsync(string message)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = new LibraryDatabaseSettings();
        config.GetSection("LibraryDatabase").Bind(settings);

        var mongoClient = new MongoClient(
                settings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            settings.DatabaseName);

        _notificationCollection = mongoDatabase.GetCollection<NotificationDTO>(
            settings.BooksCollectionName);

        _notificationCollection.InsertOne(new NotificationDTO()
        {
            Message = message,
            CreatedTime = DateTime.Now,
            MarkAsRead = false
        });

    }
}
