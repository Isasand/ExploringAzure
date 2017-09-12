using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
//using Microsoft.Azure.Management.Samples.Common;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent.Models;
using System;
using System.Linq;
using System.Text;
using System.Configuration;


using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
//using Microsoft.Azure.Relay;

namespace Quenes
{
    class Program
    {

        static void Main(string[] args)
        {
            var connectionString = "";
            string queueName = "csharpqueue"; //queue name
            string serviceBus = "IsasSpace"; //name of service buse
            var argName = SdkContext.RandomResourceName("rgSB01_", 24);
            string namespaceName = "isasNamespace";

            var namespaceManager = Microsoft.ServiceBus.NamespaceManager.CreateFromConnectionString(connectionString);

            bool rename = true;


            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            int messageChoice = 0;
            bool restart = true; //restart variable

            while (restart)
            {
                Console.Clear();
                Console.WriteLine("Connected to service bus: {0}\nConnected to queue: {1}\n", serviceBus, queueName);
                Console.WriteLine("Send message (1)\nRead message (2)");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            bool send = true;
                            while (send)
                            {
                                Console.Clear();
                                Console.Write("Input message: ");
                                string input = Console.ReadLine();
                                var message = new BrokeredMessage(input);

                                Console.WriteLine(String.Format("Message id: {0}", message.MessageId));

                                try
                                {
                                    client.Send(message);
                                    messageChoice = SentMessageMenu();
                                }

                                catch
                                {
                                    Console.WriteLine("Could not send message");
                                    send = false;
                                }

                                switch (messageChoice)
                                {
                                    case 0:
                                        {
                                            restart = true;
                                            break;
                                        }

                                    case 1:
                                        {
                                            send = true;
                                            break;
                                        }

                                    case 2:
                                        {
                                            send = false;
                                            restart = true;
                                            break;
                                        }

                                    case 3:
                                        {
                                            send = false;
                                            restart = false;
                                            break;
                                        }
                                }


                            }
                            break;
                        }

                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("Recieving messages from service bus\n");
                            client.OnMessage(readMessage =>
                            {
                                Console.WriteLine(String.Format("Message body: {0}", readMessage.GetBody<String>()));
                                Console.WriteLine(String.Format("Message id: {0}", readMessage.MessageId));
                                /*int enqueuedSequenceNumber = Convert.ToInt32(readMessage.EnqueuedSequenceNumber);
                                Console.WriteLine(enqueuedSequenceNumber);*/
                                Console.WriteLine("\n");
                            });


                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("Back to menu (1)");
                            Console.WriteLine("Exit program (2)");

                            int menuChoice = Convert.ToInt32(Console.ReadLine());

                            switch (menuChoice)
                            {
                                case 1:
                                    {
                                        restart = true;
                                        break;
                                    }
                                case 2:
                                    {
                                        restart = false;
                                        break;
                                    }
                            }
                            break;


                        }

                }

            }

        }


        public static int SentMessageMenu()
        {
            int menuChoice;
            Console.WriteLine("Message successfully sent!\n");
            Console.WriteLine("\nSend new message (1)");
            Console.WriteLine("Back to menu(2)");
            Console.WriteLine("Exit program (3)\n");
            menuChoice = Convert.ToInt32(Console.ReadLine());
            return menuChoice;
        }

    }


}