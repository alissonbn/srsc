using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace signal.netframeworkclient
{
    class Program
    {
        static HubConnection connection;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.0.7:5000/termhub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            string input = string.Empty;
            while (input != "q")
            {
                Console.WriteLine("1 to connect, 2 to send message");
                input = Console.ReadLine();
                if (input == "1")
                {
                    //connect
                    connection.On<string, string>("ReceiveMessage", (id, message) =>
                    {
                        Console.WriteLine($"id = {id}; message = {message}");
                    });

                    try
                    {
                        connection.StartAsync();
                        Console.WriteLine("Connection started");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (input == "2")
                {
                    //send message
                    try
                    {
                        Console.WriteLine("id?");
                        string id = Console.ReadLine();
                        string msg = string.Empty;
                        if (!id.StartsWith("id="))
                        {
                            Console.WriteLine("message?");
                            msg = Console.ReadLine();
                        }


                        connection.InvokeAsync("SendMessage",
                            id, msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}


