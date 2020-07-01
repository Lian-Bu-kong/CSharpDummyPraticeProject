using Akka.Actor;
using Akka.IO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DummyMonitorAkkaSys
{
    public class MonitorActor : ReceiveActor
    {

        public IActorRef connection { get; set; }

        public MonitorActor()
        {         
            ConnectToServer();
            // TCP Event
            Receive<Tcp.Connected>(message => TcpConnected(message));
            Receive<Tcp.CommandFailed>(message => TcpCommandFail(message));
            Receive<Tcp.ConnectionClosed>(message => TcpConnectionClosed(message));
            Receive<Tcp.Received>(message => TcpReceivePro(message));
        }

        private void StrPor(string msg)
        {
            Console.WriteLine($"MonitorActor Receview Msg:"+msg);
        }

        private void TcpReceivePro(Tcp.Received message)
        {
            Console.WriteLine("接收訊息觸發");

            // 解析Bytes Data 做反序列化成Obj
            var received = message as Tcp.Received;
            var bytes = received.Data.ToArray();
            var str = Encoding.Default.GetString(bytes);
            Console.WriteLine("接收訊息. Message="+ str);

            // 訊息丟回去
            connection.Tell(Tcp.Write.Create(ByteString.FromBytes(bytes)));
        }

        private void ConnectToServer()
        {
            // Connection to tcp server
            var ipAdrs = IPAddress.Parse(Configure.OutSysIp);
            var ipEndPoint = new IPEndPoint(ipAdrs, Configure.OutSysPort);
            Context.System.Tcp().Tell(new Tcp.Connect(ipEndPoint));
        }

        private void TcpConnected(Tcp.Connected message)
        {
            Console.WriteLine("TCP連線", $" TCP連線.Message =" + message.ToString());

            // Register self as connection handler
            Sender.Tell(new Tcp.Register(Self));
            connection = Sender;
        }
    
        private void TcpCommandFail(Tcp.CommandFailed message)
        {
            Console.WriteLine("TCP Command失敗", $" TCP Command失敗. Message=" + message.ToString());
            ConnectToServer();  // 重連
        }

        private void TcpConnectionClosed(Tcp.ConnectionClosed message)
        {
            Console.WriteLine("TCP關閉連線", $" Tcp關閉連線. Message=" + message.ToString());
            ConnectToServer();  // 重連
        }
    }
}
