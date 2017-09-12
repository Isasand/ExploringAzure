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


            var connectionString = "Endpoint=sb://isasspace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+ZGnzBDo8pgFWBOyyHhaywMeopvZQpx7oFA+LclLbOw=";
            var queueName = "cSharpQueue";
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            Console.Write("Input message: ");
            string input = Console.ReadLine();
            var message = new BrokeredMessage(input);

            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

            client.Send(message);
            Console.WriteLine("Message successfully sent! Press ENTER to read it or 0 to send an other one");
            if (Console.ReadLine() == "0")
            {
                Console.Write("Input message: ");
                input = Console.ReadLine();
                var message2 = new BrokeredMessage(input);

                Console.WriteLine(String.Format("Message id: {0}", message2.MessageId));

                client.Send(message2);
            }

            client.OnMessage(readMessage =>
             {
                 Console.WriteLine(String.Format("Message body: {0}", readMessage.GetBody<String>()));
                 Console.WriteLine(String.Format("Message id: {0}", readMessage.MessageId));
                 Console.WriteLine("Content type: {0}", readMessage.ContentType);
             });

            Console.WriteLine("Press ENTER to exit program");
            Console.ReadLine();

        }
    }
}
