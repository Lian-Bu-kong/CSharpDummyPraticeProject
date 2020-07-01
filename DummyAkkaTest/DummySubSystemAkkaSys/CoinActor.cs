using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummySubSystemAkkaSys
{
    public class CoinActor : ReceiveActor
    {
        public CoinActor()
        {
            Receive<string>(message => { StrPor(message); });
        }

        private void StrPor(string msg)
        {
            Console.WriteLine($"CoinActor Receview Msg:" + msg);

        }
    }
}
