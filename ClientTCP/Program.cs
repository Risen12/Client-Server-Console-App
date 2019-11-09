using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TCPCLient
            /*
            const string ip_client = "127.0.0.1";
            const int port_client = 8080;

            var TCPEndPoint = new IPEndPoint(IPAddress.Parse(ip_client), port_client);

            var TCP_Socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Write your message...");
            var message = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(message);

            TCP_Socket_client.Connect(TCPEndPoint);

            TCP_Socket_client.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = TCP_Socket_client.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            } while (TCP_Socket_client.Available > 0);

            Console.WriteLine(answer);

            TCP_Socket_client.Shutdown(SocketShutdown.Both);
            TCP_Socket_client.Close();

            Console.ReadLine();*/
            #endregion

            #region UDPClient

            //const string ip_client = "127.0.0.1";
            //const int port_client = 8082;

            //var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip_client), port_client);

            //var udp_client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //udp_client_socket.Bind(udpEndPoint);

            //while (true)
            //{
            //    Console.WriteLine("Write your message...");
            //    var message = Console.ReadLine();

            //    EndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(ip_client), 8081);
            //    udp_client_socket.SendTo(Encoding.UTF8.GetBytes(message),serverEndpoint);

            //    var buffer = new byte[256];
            //    var size = 0;
            //    var data = new StringBuilder();

            //    EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

            //    do
            //    {
            //        size = udp_client_socket.ReceiveFrom(buffer, ref serverEndpoint);
            //        data.Append(Encoding.UTF8.GetString(buffer));
            //    } while (udp_client_socket.Available > 0);

            //    Console.WriteLine(data);
            //    Console.ReadLine();
            //}
            #endregion

            const string  ip_address_client = "127.0.0.1";
            const int port = 904;

            while (true)
            {
                IPEndPoint end_client = new IPEndPoint(IPAddress.Parse(ip_address_client), port);

                Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Поддержка вас слышит, введите вашу претензию/предложение!");
                string message = Console.ReadLine();

                var data = Encoding.UTF8.GetBytes(message);
                socket_client.Connect(end_client);
                socket_client.Send(data);

                byte[] buffer = new byte[256]; // массив байт куда будем загружать данные, которые принял сокет выше
                int size = 0; // вычислим размер полученного сообщения, чтоб не тратить лишнюю память
                StringBuilder answer = new StringBuilder(); // строка, в которую поместим полученное раскодированное сообщение

                try
                {
                    do
                    {
                        size = socket_client.Receive(buffer); // узнаем размер полученной информации
                        answer.Append(Encoding.UTF8.GetString(buffer, 0, size)); // переносим информацию в строку для работы с ней

                    } while (socket_client.Available > 0); // пока полученное сообщение не передалось полностью

                    Console.WriteLine(answer);

                    socket_client.Shutdown(SocketShutdown.Both);
                    socket_client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
