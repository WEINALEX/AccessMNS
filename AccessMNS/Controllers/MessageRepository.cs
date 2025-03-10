using AccessMNS.Models;
using AccessMNS.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AccessMNS.Controllers
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesByCanalAsync(int idCanaux);
        Task<Message> AddMessageAsync(Message message);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messagesCollection;

        public MessageRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _messagesCollection = database.GetCollection<Message>(settings.Value.MessagesCollection);
        }

        public async Task<List<Message>> GetMessagesByCanalAsync(int idCanaux)
        {
            return await _messagesCollection
                .Find(m => m.IdCanaux == idCanaux)
                .SortBy(m => m.DateHeureEnvoi)
                .ToListAsync();
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            await _messagesCollection.InsertOneAsync(message);
            return message;
        }
    }
}
