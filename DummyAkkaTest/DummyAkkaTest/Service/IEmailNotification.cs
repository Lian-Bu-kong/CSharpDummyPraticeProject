using System;
using System.Collections.Generic;
using System.Text;

namespace DummyAkkaTest
{
    public interface IEmailNotification
    {
        void Send(string message);
    }
}
