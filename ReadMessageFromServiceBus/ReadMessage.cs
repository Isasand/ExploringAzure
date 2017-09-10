using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus.Messaging;

namespace Quenes
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ""; 
            var queueName = "";
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            client.OnMessage(message =>
            {
                Console.WriteLine(String.Format("Message body: {0}", message.GetBody<String>()));
                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));
            });
            Console.WriteLine("Press ENTER to exit program");
            Console.ReadLine();
        }
    }
}
