using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using System;

namespace DummySubSystemAkkaSys
{
    class Program
    {
        static void Main(string[] args)
        {
            Configure.DIManagerSetting();

            // Create method takes a name for the new actor system (System Actor Manager Master) - 需下載 Akka Remote
            var actorSystem = ActorSystem.Create(Configure.AkaSysName, Configure.AkkaConfig(Configure.AkaSysPort));
            // 註:需安裝Akka DI Extension
            actorSystem.UseServiceProvider(Configure.provider);

            // Create an instance of the actor
            var actor = actorSystem.ActorOf(actorSystem.DI().Props<CoinActor>(), nameof(CoinActor));

            // Console Input
            Console.WriteLine("Enter message");
            while (true)
            {
                var message = Console.ReadLine();

                if (message.Equals(string.Empty))
                    continue;

                if (message == "q") break;

                // Info Coil Actor
                actor.Tell(message);
            };



            Console.ReadLine();
            actorSystem.Stop(actor);
        }
    }
}
