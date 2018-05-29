using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace MongoDBCommandsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase("test");
            var commandText = Console.ReadLine();
            // { find: 'Abonent', filter: { "Fio" : "Конюхов В.С." }}
            // {insert: 'Abonent', documents: [{ "StreetCD" : 3.0, "HouseNo" : 1.0,"FlatNo" : 65.0,"Fio" : "Конюхов В.С.1","Phone" : "761699" }]}
            var command = new BsonDocument(BsonSerializer.Deserialize<BsonDocument>(commandText));
            Console.WriteLine(database.RunCommand<BsonDocument>(command).ToString());
            do
            {
                Console.ReadKey();
            } while (Console.ReadKey().KeyChar != 81);
        }
    }
}
