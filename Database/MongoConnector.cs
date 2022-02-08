using MongoDB.Driver;

namespace CSUserAPI.Database
{
	//singleton class so only one instance is running at a time
    public static class MongoConnector
    {
		public static readonly MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
		public static readonly IMongoDatabase mongoDb = mongoClient.GetDatabase("MainDB");
	}
	
	}

