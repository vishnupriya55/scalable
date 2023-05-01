using System.Text;
using LibraryService.Models;
using RabbitMQ.Client;

namespace LibraryService.Services
{
    public class NotifyUsers
    {
        const string QueueName = "Notification";
        public void SendNotification(string NotificationType, BookDTO bookDetails)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QueueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            string message = GenerateQueueMessage(NotificationType, bookDetails);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: string.Empty,
                     routingKey: QueueName,
                     basicProperties: null,
                     body: body);
        }

        private static string GenerateQueueMessage(string NotificationType, BookDTO bookDetails)
        {
            return NotificationType switch
            {
                "Create" => $"Hi User, We are glad to inform you that \"{bookDetails.Title}\" is now available in the library.",
                "Update" => $"Hi User, We would like to inform you that details of book with Title: \"{bookDetails.Title}\" has been updated.",
                "HurryUp" => $"Hi Users, Hurry up and purchase book with Title: \"{bookDetails.Title}\", limited stock if left ({bookDetails.StockLeft}).",
                "OutOfStock" => $"Hi User, We would like to inform you that book with Title: \"{bookDetails.Title}\" is out of stock.",
                "Delete" => $"Hi User, We are sorry to inform that \"{bookDetails.Title}\" is no longer available in the library.",
                _ => "Hey User, This is a sample notification",
            };
        }
    }
}
