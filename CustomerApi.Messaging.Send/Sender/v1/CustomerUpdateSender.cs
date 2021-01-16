using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSender : ICustomerUpdateSender
    {
        private readonly string _hostname;
        private readonly string _queuName;
        private readonly string _userName;
        private readonly string _password;

        private IConnection _connection;

        public CustomerUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queuName = rabbitMqOptions.Value.QueueName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        public void SendCustomer(Customer customer)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queuName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    // prepare data for publishing
                    var json = JsonConvert.SerializeObject(customer);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queuName, basicProperties: null, body: body);
                }
            }
            else
            {
                // TODO: think about handling this situations
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _userName,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if(_connection != null)
            {
                return true;
            }

            // try to create connection
            CreateConnection();

            return _connection != null;
        }
    }
}
