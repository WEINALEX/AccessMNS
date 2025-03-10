using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AccessMNS.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("contenuMessage")]
        public string ContenuMessage { get; set; } = string.Empty;

        [BsonElement("dateHeureEnvoi")]
        public DateTime DateHeureEnvoi { get; set; } = DateTime.UtcNow;

        [BsonElement("idUser")]
        public int IdUser { get; set; }

        [BsonElement("idCanaux")]
        public int IdCanaux { get; set; }

        [BsonElement("modification")]
        public bool Modification { get; set; } = false;
    }
}