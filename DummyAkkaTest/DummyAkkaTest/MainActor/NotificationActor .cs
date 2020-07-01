using Akka.Actor;
using Akka.DI.Core;
using DummyAkkaTest.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyAkkaTest
{
    class NotificationActor : UntypedActor
    {
        // DI
        private readonly IEmailNotification emailNotification;
        // Child Actor
        private readonly IActorRef childActor;

        public NotificationActor(IEmailNotification emailNotification)
        {
            this.emailNotification = emailNotification;
            this.childActor = Context.ActorOf(Context.System.DI().Props<TextNotificationActor>());
        }


        protected override void OnReceive(object message)
        {

            Console.WriteLine($"Message received {message}");
            emailNotification.Send(message.ToString());


            if (message.ToString().ToLower().Equals("object"))
            {
                message = new TextMessage
                {
                    txt = "TextMessage Object Snd Test"
                };
            }
               

            // Infor Text Actor
            childActor.Tell(message);
        }




        protected override SupervisorStrategy SupervisorStrategy()
        {
            // Retries to 10 times within a minute
            // This is the number of times the child actor is allowed to restart within the time window specified. 
            // The negative value means no limit.
            return new OneForOneStrategy(
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    return ex switch
                    {
                        // Actor Life Operator
                        // the actor will resume as if nothing happened
                        ArgumentException ae => Directive.Resume,
                        //  it will restart the actor and move on. And for any other unknown exception, it will stop the actor.
                        NullReferenceException ne => Directive.Restart, // Restart New Instance

                        _ => Directive.Stop
                    };
                }
                );
        }

        protected override void PreStart() => Console.WriteLine("NotificationActor started");

        protected override void PostStop() => Console.WriteLine("NotificationActor stopped");

        protected override void PreRestart(Exception reason, object message) => Console.WriteLine("Reason:"+reason+":"+"Object"+message);

    }
}
