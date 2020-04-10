using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks.Util
{
    static class TcpUdpClientHelper
    {
        static public IPAddress LocalAddress(this TcpClient client)
        {
            return ((IPEndPoint)client.Client.LocalEndPoint).Address;
        }
        static public int LocalPort(this TcpClient client)
        {
            return ((IPEndPoint)client.Client.LocalEndPoint).Port;
        }
        static public IPEndPoint LocalEndPoint(this TcpClient client)
        {
            return (IPEndPoint)client.Client.LocalEndPoint;
        }
        static public void Send(this TcpClient client, byte[] bs)
        {
            client.GetStream().Write(bs, 0, bs.Count());
        }
        static public int Receive(this TcpClient client, byte[] bs, int MaxCntRecv)
        {
            var recvCnt = client.GetStream().Read(bs, 0, MaxCntRecv);
            return recvCnt;
        }





        static public IPAddress LocalAddress(this UdpClient client)
        {
            return ((IPEndPoint)client.Client.LocalEndPoint).Address;
        }
        static public int LocalPort(this UdpClient client)
        {
            return ((IPEndPoint)client.Client.LocalEndPoint).Port;
        }
        static public IPEndPoint LocalEndPoint(this UdpClient client)
        {
            return (IPEndPoint)client.Client.LocalEndPoint;
        }

        static public IPAddress LocalAddress(this Socket client)
        {
            return ((IPEndPoint)client.LocalEndPoint).Address;
        }
        static public int LocalPort(this Socket client)
        {
            return ((IPEndPoint)client.LocalEndPoint).Port;
        }
        static public IPEndPoint LocalEndPoint(this Socket client)
        {
            return (IPEndPoint)client.LocalEndPoint;
        }
    }
}
