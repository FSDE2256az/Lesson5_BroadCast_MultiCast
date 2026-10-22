using System.Net;
using System.Text;
using System.Net.Sockets;


UdpClient sender = new UdpClient();
IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 12345);

while (true)
{
    Console.Write("Enter your message: ");
    string? message = Console.ReadLine();

    if (message is null)
        continue;

    byte[] bytes = Encoding.Default.GetBytes(message);
    sender.Send(bytes, bytes.Length, endPoint);
}