using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Configuration;
using System.Linq.Expressions;
using System.Text;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Test.RentMotorCycles.Infrastructure
{
    public partial class Factory
    {
     
        protected async void SetPublisher(String queueName, String message)
        {
            IModel channel = Connection().CreateModel();

            channel.ConfirmSelect();

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true; // Mensagem persistente

            channel.BasicNacks += (sender, ea) =>
            {
                //Console.WriteLine($"Cadastrado com sucesso:");
            };

            channel.BasicAcks += (sender, ea) =>
            {
                //Console.WriteLine("**NACK***");
            };

            var _me = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange:"", 
                                 routingKey: queueName, 
                                 mandatory: true,
                                 basicProperties: properties, 
                                 body: _me);

        }


        protected IConnection Connection()
        {
            try
            {
                IConfigurationRoot configuration =  new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                string urlRabbitMQ = ConfigurationManager.AppSettings["urlRabbitMQ"] ?? configuration.GetConnectionString("urlRabbitMQ");


                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri(urlRabbitMQ);

                return factory.CreateConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}