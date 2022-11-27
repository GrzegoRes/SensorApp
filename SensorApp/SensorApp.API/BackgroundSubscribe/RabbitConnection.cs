using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SensorApp.API.Repository;
using SensorApp.API.Repository.Entity;

namespace SensorApp.API.BackgroundSubscribe
{
    public class RabbitConnection : BackgroundService
    {
        private ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRepositorySensor _repository;

        public RabbitConnection(IOptions<SettingsRB> options, IRepositorySensor repositorySensor)
        {
            _factory = new ConnectionFactory()
            {
                HostName = options.Value.HostName,
                Port = Int32.Parse(options.Value.Port),
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: options.Value.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _repository = repositorySensor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _channel.Dispose();
                _connection.Dispose();
                return Task.CompletedTask;
            }
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);

                ConvertAndSave(message);

            };
            //autoAck - kiedy zsotanie obsłuzona z kolejki
            _channel.BasicConsume(queue: "sensor.02", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        private void ConvertAndSave(string message)
        {
            Task.Run(() =>
            {
                var chunks = message.Split("|");

                var sensor = new SensorDB();
                if (chunks.Length == 4)
                {
                    sensor.name = chunks[0];
                    sensor.value = Int32.Parse(chunks[2]);
                    sensor.type = chunks[1];
                    sensor.dateGenerate = DateTime.Now;
                }

                _repository.save(sensor);
            });
        }
    }
}
