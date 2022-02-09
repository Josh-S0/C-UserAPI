using MongoDB.Driver;

namespace CSUserAPI.Database
{
    public class MongoConnector
    {
		public static readonly MongoClient mongoClient = new("mongodb://localhost:27017");
		public static readonly IMongoDatabase mongoDb = mongoClient.GetDatabase("MainDB");
	}
	
	}

