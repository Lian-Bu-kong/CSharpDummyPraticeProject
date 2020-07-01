using Akka.Actor;
using DummyAkkaTest.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

// child actors
namespace DummyAkkaTest
{
    class TextNotificationActor : ReceiveActor
    {

        public TextNotificationActor()
        {
            Receive<string>(MsgBoxResult => ProStr(MsgBoxResult)); 

            Receive<TextMessage>(MsgBoxResult => ProTextMessage(MsgBoxResult));
        }

      
        private void ProStr(string msg)
        {
            // Expection Test
            if (msg.Equals("n"))
                throw new NullReferenceException(); //Noti Actor supervision strategy the child actor will be Restar.
            if (msg.Equals("e"))
                throw new ArgumentException();      //Noti Actor supervision strategy the child actor will be Resume.

            Console.WriteLine($"TextNotification Sending  message {msg}");
        }

        private void ProTextMessage(TextMessage msg)
        {
            Console.WriteLine($"TextNotification Sending Text object message  {msg.txt}");
        }


        protected override void PreStart() =>
          Console.WriteLine("TextNotification child started");

        protected override void PostStop() =>
            Console.WriteLine("TextNotification child stopped");

    }
}
