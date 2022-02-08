using MongoDB.Driver;

namespace CSUserAPI.Database
{
	//singleton class so only one instance is running at a time
    public sealed class MongoConnector
    {
		private static MongoConnector? single_instance = null;
		private static readonly object padlock = new object();
		private IMongoDatabase mongoDb;
		
		public MongoClient mongoClient;

		private MongoConnector()
		{
			this.mongoClient = new MongoClient("mongodb://localhost:27017");
			this.mongoDb = mongoClient.GetDatabase("MainDB");
		}
		
		public static MongoConnector getInstance
		{
            get{
				lock (padlock)
                {
					if (single_instance == null)
						{
							single_instance = new MongoConnector();
						}
					return single_instance;
                }
            }
			
		}

		
	}
}
