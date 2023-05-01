using LibraryService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LibraryService.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<BookDTO> _booksCollection;
        private readonly NotifyUsers _notifyUsers;
        public BooksService(
            IOptions<LibraryDatabaseSettings> LibraryDatabaseSettings, NotifyUsers notifyUsers)
        {
            var mongoClient = new MongoClient(
                LibraryDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LibraryDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<BookDTO>(
                LibraryDatabaseSettings.Value.BooksCollectionName);
            _notifyUsers = notifyUsers;
        }

        public async Task<List<BookDTO>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<BookDTO?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BookDTO newBook)
        {
            await _booksCollection.InsertOneAsync(newBook);
            _notifyUsers.SendNotification("Create", newBook);
        }
        public async Task UpdateAsync(string id, BookDTO updatedBook)
        {
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);
            SendNotificationBasedOnStock(updatedBook);
        }

        public async Task RemoveAsync(string id, BookDTO bookDetails)
        {
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
            _notifyUsers.SendNotification("Delete", bookDetails);
        }

        private void SendNotificationBasedOnStock(BookDTO updatedBook)
        {
            switch (updatedBook.StockLeft)
            {
                case 0:
                    _notifyUsers.SendNotification("OutOfStock", updatedBook);
                    break;
                case <= 15:
                    _notifyUsers.SendNotification("HurryUp", updatedBook);
                    break;
                default:
                    _notifyUsers.SendNotification("Update", updatedBook);
                    break;
            }
        }

    }

}