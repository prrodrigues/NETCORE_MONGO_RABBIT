using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Infrastructure;
using Test.RentMotorCycles.Service;

namespace Test.RentMotorCycles.RabbitMQConsumer;

public class ConsumerMoto : Factory
{

    public static IMotoService _motoService;
    public static IModel channel;

    public ConsumerMoto()
    {
        _motoService = new MotoService();
        SetConsumer();
    }


    protected void SetConsumer()
    {
        channel = Connection().CreateModel();

        String queueName = "createMoto";

        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

        consumer.Received += received;

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }


    public static void received(object model, BasicDeliverEventArgs ea) {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                MotoEvento moto = Newtonsoft.Json.JsonConvert.DeserializeObject<MotoEvento>(message);

                Moto ret = new Moto()
                {
                    identificador = moto.identificador,
                    ano = moto.ano,
                    modelo = moto.modelo,
                    placa = moto.placa,
                };
                
                _motoService.SetMoto(ret);

                Console.Write(message);

                if(ret.ano == 2024){
                    Console.Write(2024);

                    MotoEvento me = new MotoEvento {
                        identificador = ret.identificador,
                        ano = ret.ano,
                        modelo = ret.modelo,
                        placa = ret.placa
                    };

                    _motoService.SetMotoNovo(me);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                channel.BasicNack(ea.DeliveryTag, false, false);
            }
        }

}
