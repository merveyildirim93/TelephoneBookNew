using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using TelephoneBook.DataAccess.Context;
using TelephoneBook.DataAccess.Entity;

namespace TelephoneBook.Helper.RabbitMq
{
    public class RabbitMq
    {
        public static async void Consume(Report obj, PostreSqlContext database)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                //Kuyruk oluşturma
                channel.QueueDeclare(queue: "ReportCreateQueue",
                    durable: true, //Data fiziksel olarak mı yoksa memoryde mi tutulsun
                    exclusive: false, //Başka connectionlarda bu kuyruğa ulaşabilsin mi
                    autoDelete: false, //Eğer kuyruktaki son mesaj ulaştığında kuyruğun silinmesini istiyorsak kullanılır.
                    arguments: null);//Exchangelere verilecek olan parametreler tanımlamak için kullanılır.
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                string message = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(message);
                Report model = JsonConvert.DeserializeObject<Report>(message);
                if (model == null)
                {
                    return;
                }
                else
                {
                    await database.Report.AddAsync(obj);
                    database.SaveChanges();
                }
                //Queue ya atmak için kullanılır.
                channel.BasicPublish(exchange: "",//mesajın alınıp bir veya birden fazla queue ya konmasını sağlıyor.
                    routingKey: "ReportCreateQueue", //Hangi queue ya atanacak.
                    properties,
                    body: body);//Mesaj içeriği
            }

        }
    }
}
