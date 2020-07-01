using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using System;

/**
  簡易Actor測設
    MainActor : NotificationActor
    ChildrenActor : TextNotificationActor
  
   測試1: Console輸入text ->通知NotificationActor , NotificationActor通知TextNotificationActor
   測試2: SupervisorStrategy管理ChildAcotr生命週期事件處理機制實驗
        NullReferenceException : Children Restart
        ArgumentException : Children Resume
   測試3: Actor send object溝通 TextMessage
 */

namespace DummyAkkaTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Configure.DIManagerSetting();

            // Create method takes a name for the new actor system (System Actor Manager Master)
            var actorSystem = ActorSystem.Create("test-actor-system");
            // 註:需安裝Akka DI Extension
            actorSystem.UseServiceProvider(Configure.provider);

            // Create an instance of the actor
            var actor = actorSystem.ActorOf(actorSystem.DI().Props<NotificationActor>(), "main-actor");


            // Console Input
            Console.WriteLine("Enter message");
            while (true)
            {
                var message = Console.ReadLine();
                
                if (message.Equals(string.Empty))
                    continue;
                
                if (message == "q") break;

                // Info Noti Actor
                actor.Tell(message);
            };
            Console.ReadLine();
            actorSystem.Stop(actor);

        }
    }
}
