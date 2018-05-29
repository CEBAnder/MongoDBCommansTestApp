using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Configuration;

namespace MongoDBCommandsTestApp
{
    class CommandExecutor
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;

        public static bool CompareCommandsResults (string FirstCommand, string SecondCommand)
        {
            return GetCommandResult(FirstCommand).Equals(GetCommandResult(SecondCommand));
        }

        public static BsonDocument GetCommandResult (string MongoCommand)
        {
            MongoClient client = new MongoClient(connectionString);
            var database = client.GetDatabase("test");
            var command = new BsonDocument(BsonSerializer.Deserialize<BsonDocument>(GetCommandParams(MongoCommand)));
            var gotCredentials = false;
            // users only allowed to run 'find' queryes
            foreach (var element in command.Elements)
            {
                if (element.Name == "find")
                    gotCredentials = true;
            }
            if (!gotCredentials)
                throw new System.UnauthorizedAccessException();
            return database.RunCommand<BsonDocument>(command);
        }

        private static string GetCommandParams (string MongoCommand)
        {
            //query must be like db.runCommand(...);
            if (!MongoCommand.StartsWith("db.runCommand"))
                throw new System.FormatException();
            var partOfCommand = MongoCommand.Split('(')[1];
            return partOfCommand.Substring(0, partOfCommand.LastIndexOf(')'));
        }
    }
}
