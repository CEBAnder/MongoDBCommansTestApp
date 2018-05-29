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
            try
            {
                var str1 = Console.ReadLine();
                var str2 = Console.ReadLine();
                if (CommandExecutor.CompareCommandsResults(str1, str2))
                    Console.WriteLine("Ok");
                else
                    Console.WriteLine("Different results");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                int keychar = new int();
                do
                {
                    keychar = Console.ReadKey().KeyChar;
                } while (keychar != 81);
            }
        }
    }
}
