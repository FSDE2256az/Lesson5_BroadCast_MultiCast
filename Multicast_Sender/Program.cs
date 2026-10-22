using System.Net;
using System.Text;
using System.Net.Sockets;

IPAddress multicastIPAddress = IPAddress.Parse("239.0.0.1");
int port = 12345;

using UdpClient udpClient = new UdpClient();

udpClient.JoinMulticastGroup(multicastIPAddress);

Console.WriteLine("Multicast server working...");

while (true)
{
    Console.Write("Enter the message to be sent: ");
    string? message = Console.ReadLine();

    if (message is null)
        continue;

    byte[] bytes = Encoding.UTF8.GetBytes(message);

    udpClient.Send(
        bytes,
        bytes.Length,
        new IPEndPoint(multicastIPAddress, port)
    );

    if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        // Köhnə multicast qrupundan çıx
        udpClient.DropMulticastGroup(multicastIPAddress);

        Console.WriteLine("Old multicast group left.");

        // Yeni multicast qrupuna qoşul
        multicastIPAddress = IPAddress.Parse("239.0.0.1");
        port = 22345;

        udpClient.JoinMulticastGroup(multicastIPAddress);

        Console.WriteLine("Joined new multicast group.");
    }

    Console.WriteLine("Message sent.");
}