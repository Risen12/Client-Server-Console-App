using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ServerTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string address_support = "127.0.0.1"; // адрес поддержки(сервера)
            const int port_support = 904; // пор поддержки(сервера)

            IPEndPoint support_endpoint = new IPEndPoint(IPAddress.Any,port_support); // Точка подключения,нужна по сути для паузы

            var Socket_support = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp); // Сам сокет, штука - посредник между сервером и протоколом(TCP/UDP)

            Socket_support.Bind(support_endpoint); // Связываем сокет сервера с точкой подключения
            Socket_support.Listen(5); // Режим ожидания, слушает конкретно данный ендпоинт(указанный в связке сверху)
            Socket lisntener;
            bool first = true;

            while (true)
            {
                try
                {
                    StringBuilder message = new StringBuilder();
                    lisntener = Socket_support.Accept(); // создаем новый сокет для принятия информации от клиента
                    byte[] buffer = new byte[256]; // массив байт куда будем загружать данные, которые принял сокет выше
                    int size = 0; // вычислим размер полученного сообщения, чтоб не тратить лишнюю память
                    StringBuilder data = new StringBuilder(); // строка, в которую поместим полученное раскодированное сообщение
                    do
                    {
                        size = lisntener.Receive(buffer);// узнаем размер полученной информации
                        data.Append(Encoding.UTF8.GetString(buffer, 0, size)); // переносим информацию в строку для работы с ней

                    } while (lisntener.Available > 0); // пока полученное сообщение не передалось полностью

                    //lisntener.Send(Encoding.UTF8.GetBytes("Ваше сообщение доставлено, ожидайте ответа \n"));  // Отправляем клиенту информации об успешной передачи
                    Console.WriteLine(data); // пишем полученное сообщение в консоль
                    message.Append("Сервер принял ваш сигнал, ожидайте ответа! \n");
                    Regex reg = new Regex(@"\S*проб\S*");
                    MatchCollection matches = reg.Matches(data.ToString());
                    if (matches.Count > 0)
                    {
                        message.Append("В чём конкретно у вас проблема? \n");
                    }

                    reg = new Regex(@"\S*подкл\S*");
                    matches = reg.Matches(data.ToString());
                    if (matches.Count > 0)
                    {
                        message.Append("Попробуйте переподключиться! \n");
                    }

                    lisntener.Send(Encoding.UTF8.GetBytes(message.ToString()));

                    lisntener.Shutdown(SocketShutdown.Both); //  выключаем передачу и приём информации у сокета, который принимает инфу
                    lisntener.Close(); // закрываем соединение
                }
                catch (Exception e) // отлов ошибок
                {
                    Console.WriteLine(e.Message);
                }
            }

        
        }


     }

}


#region TCPServer
/*
const string ip_server = "127.0.0.1";
const int port_server = 8080;

var TCPEndPoint = new IPEndPoint(IPAddress.Parse(ip_server),port_server);

var TCP_server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

TCP_server_socket.Bind(TCPEndPoint);
TCP_server_socket.Listen(6);

while (true)
{
    var listener = TCP_server_socket.Accept();
    var buffer = new byte[256];
    var size = 0;
    var data = new StringBuilder();

    do
    {
        size = listener.Receive(buffer);
        data.Append(Encoding.UTF8.GetString(buffer,0,size));

    } while (listener.Available > 0);

    Console.WriteLine(data.ToString());

    listener.Send(Encoding.UTF8.GetBytes("Success"));

    listener.Shutdown(SocketShutdown.Both);
    listener.Close(); 
    }*/
#endregion

#region UDPServer
//const string ip_server = "127.0.0.1";
//const int port_server = 8081;

//var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip_server), port_server);

//var udp_server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//udp_server_socket.Bind(udpEndPoint);

//while (true)
//{
//    var buffer = new byte[256];
//    var size = 0;
//    var data = new StringBuilder();

//    EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

//    do
//    {
//        size = udp_server_socket.ReceiveFrom(buffer, ref senderEndPoint);
//        data.Append(Encoding.UTF8.GetString(buffer));
//    } while (udp_server_socket.Available > 0);

//    udp_server_socket.SendTo(Encoding.UTF8.GetBytes("Success"),senderEndPoint);

//    Console.WriteLine(data);
//}
#endregion


