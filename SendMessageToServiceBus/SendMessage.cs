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
            var queueName = "isasko";
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);
            
            var message = new BrokeredMessage("This is a test message!");

            Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

            client.Send(message);

            Console.WriteLine("Message successfully sent! Press ENTER to read it");
          
            Console.WriteLine("Press ENTER to exit program");
            Console.ReadLine();
        }
    }
}
 