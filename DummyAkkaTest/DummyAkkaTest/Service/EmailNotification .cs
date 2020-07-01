using System;
using System.Collections.Generic;
using System.Text;

namespace DummyAkkaTest
{
    public class EmailNotification : IEmailNotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email sent with message {message}");
        }
    }
     
}
