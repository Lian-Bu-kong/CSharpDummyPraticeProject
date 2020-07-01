using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyAkkaTest
{
    public static class Configure
    {

        public static ServiceProvider provider;

        public static void DIManagerSetting()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IEmailNotification, EmailNotification>();
            collection.AddScoped<NotificationActor>();
            collection.AddScoped<TextNotificationActor>();  // 注意Actor會重新Req New Instance. 請使用AddScoped
            provider = collection.BuildServiceProvider();
        }

    }
}
