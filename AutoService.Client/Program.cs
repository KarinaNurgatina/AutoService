using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Client
{
    static void Main()
    {
        // Подключение к серверу
        TcpChannel channel = new TcpChannel();
        ChannelServices.RegisterChannel(channel, false);

        RequestManager requestManager = (RequestManager)Activator.GetObject(
            typeof(RequestManager),
            "tcp://localhost:8086/RequestManager"
        );

        if (requestManager == null)
        {
            Console.WriteLine("Ошибка подключения к серверу.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Добавить заявку");
            Console.WriteLine("2. Просмотреть заявки");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите описание заявки: ");
                    string request = Console.ReadLine();
                    requestManager.AddRequest(request);
                    Console.WriteLine("Заявка добавлена!");
                    break;
                case "2":
                    var requests = requestManager.GetRequests();
                    Console.WriteLine("Список заявок:");
                    foreach (var r in requests)
                    {
                        Console.WriteLine($"- {r}");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод, попробуйте снова.");
                    break;
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
