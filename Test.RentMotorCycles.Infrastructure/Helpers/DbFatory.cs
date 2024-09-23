using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Configuration;
using System.Linq.Expressions;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Test.RentMotorCycles.Infrastructure
{
    public partial class Factory
    {
        private string message {get;set;} = "";
        
        protected void InsertOne<T>(Object obj){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);

            coll.InsertOne((T)obj);
        }

        protected void UpdateOne<T>(Expression<Func<T,bool>> expr, Object obj){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);
            coll.ReplaceOne<T>(expr, (T)obj);
        }

        protected void UpdateFields<T>(Expression<Func<T,bool>> whereClause, UpdateDefinition<T> updateFields, T obj){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);
            coll.UpdateOne<T>(whereClause, updateFields);
        }


        protected void DeleteOne<T>(Expression<Func<T,bool>> expr){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);
            coll.DeleteOne<T>(expr);
        }


        protected List<T> Find<T>(Expression<Func<T,bool>> expr){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);
            FilterDefinition<T> fd = Builders<T>.Filter.Where(expr);
            return coll.Find(fd).ToList();
        }

        protected List<T> FindAll<T>(){
            IMongoDatabase db = CreateConnection();
            var coll = db.GetCollection<T>(typeof(T).Name);
            return coll.Find(Builders<T>.Filter.Empty).ToList();
        }


        private IMongoDatabase CreateConnection()
        {
            try
            {
                IConfigurationRoot configuration =  new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = ConfigurationManager.AppSettings["connString"] ?? configuration.GetConnectionString("connString");

                var mongoUrl = new MongoUrl(connectionString);
                var dbname = mongoUrl.DatabaseName;
                return new MongoClient(mongoUrl).GetDatabase(dbname);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
