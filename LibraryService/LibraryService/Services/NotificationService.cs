using LibraryService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryService.Services
{
    public class NotificationService
    {
        private readonly IMongoCollection<NotificationDTO> _notificationCollection;
        public NotificationService(
            IOptions<LibraryDatabaseSettings> LibraryDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                LibraryDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LibraryDatabaseSettings.Value.DatabaseName);

            _notificationCollection = mongoDatabase.GetCollection<NotificationDTO>(
                LibraryDatabaseSettings.Value.NotificationCollectionName);
            
        }

        public async Task<List<NotificationDTO>> GetAsync() =>
            await _notificationCollection.Find(_ => true).ToListAsync();
    }

}