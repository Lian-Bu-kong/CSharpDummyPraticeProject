using System;
using Autofac;
using Autofac.Core;

namespace AutofacSamples
{
    class Program
    {
        public interface ILog
        {
            void Write(string message);
        }

        public interface IConsole
        {

        }

        public class ConsoleLog : ILog, IConsole
        {
            public void Write(string message)
            {
                System.Console.WriteLine(message);
            }
        }

        public class EmailLog : ILog
        {
            private const string adminEmail = "admin@foo.com";
            public void Write(string message)
            {
                System.Console.WriteLine($"Email sent to {adminEmail} : {message}");
            }
        }

        public class SMSLog : ILog
        {
            private string _phoneNumber;

            public SMSLog(string phoneNumber)
            {
                this._phoneNumber = phoneNumber;
            }

            public void Write(string message)
            {
                Console.WriteLine($"SMS to {_phoneNumber} : {message}");
            }
        }

        public class Engine
        {
            private ILog Log { get; set; }

            private int Id { get; set; }

            public Engine(ILog log)
            {
                Log = log;
                Id = new Random().Next();
            }

            public Engine(ILog log, int id)
            {
                Log = log;
                Id = id;
            }

            public void Ahead(int power)
            {
                Log.Write($"Engine [{Id}] ahead {power}");
            }
        }

        public class Car
        {

            private ILog Log { get; set; }

            private Engine Engine { get; set; }

            public Car(Engine engine)
            {
                Engine = engine;
                Log = new EmailLog();
            }

            public Car(Engine engine, ILog log)
            {
                Engine = engine;
                Log = log;
            }

            public void Go()
            {
                Engine.Ahead(100);
                Log.Write("Car going forward...");
            }
        }

        static void Main(string[] args)
        {
            //TestAsSelf();
            //TestMultiInterface();
            //TestConstructor();
            //TestRegisterInstance();
            //TestNameParameter();
            //TestTypeParameter();
            //TestResolvedParameter();
            //TestNameParameter2();

            DelegateFactorySample.Demo();

            // 傳統寫法
            //var log = new ConsoleLog();
            //var engine = new Engine(log);
            //var car = new Car(engine, log);
            //car.Go();

            Console.ReadKey();
        }

        /// <summary>
        /// AsSelf()可以解析自己
        /// </summary>
        private static void TestAsSelf()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            var container = builder.Build();

            // 如果沒加AsSelf()這個方法的話，程式執行會報錯誤
            var consoleLog = container.Resolve<ConsoleLog>();
            var iLog = container.Resolve<ILog>();
            consoleLog.Write($"HashCode ConsoleLog: {consoleLog.GetHashCode()}, ILog: {iLog.GetHashCode()}");
            var car = container.Resolve<Car>();
            car.Go();
        }

        /// <summary>
        /// 測試多個Interface實作
        /// </summary>
        private static void TestMultiInterface()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>()
                .As<ILog>()
                .As<IConsole>()
                .AsSelf();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            var container = builder.Build();

            // 如果沒加AsSelf()這個方法的話，程式執行會報錯誤
            var consoleLog = container.Resolve<ConsoleLog>();
            var iLog = container.Resolve<ILog>();
            var iConsole = container.Resolve<IConsole>();
            consoleLog.Write($"HashCode ConsoleLog: {consoleLog.GetHashCode()}, " +
                             $"ILog: {iLog.GetHashCode()}, " +
                             $"IConsole: {iConsole.GetHashCode()}");
            var car = container.Resolve<Car>();
            car.Go();
        }

        /// <summary>
        /// 選擇建構子注入
        /// </summary>
        private static void TestConstructor()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>()
            // 選擇要注入的建構子
            //.UsingConstructor(typeof(Engine));
                .UsingConstructor(typeof(Engine), typeof(ILog));

            var container = builder.Build();
            var car = container.Resolve<Car>();
            car.Go();
        }

        /// <summary>
        /// 註冊實體
        /// </summary>
        private static void TestRegisterInstance()
        {
            var builder = new ContainerBuilder();

            // 將實體註冊成元件(Component)，並將ILog和IConsole型別也註冊到
            var log = new ConsoleLog();
            builder.RegisterInstance(log)
                .As<ILog>()
                .As<IConsole>();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            var container = builder.Build();

            // 取得的ILog和IConsole為同一個元件(Component)
            var iLog = container.Resolve<ILog>();
            var iConsole = container.Resolve<IConsole>();
            iLog.Write($"ILog: {iLog.GetHashCode()}, IConsole: {iConsole.GetHashCode()}");
        }

        /// <summary>
        /// 使用Lambda註冊元件
        /// </summary>
        private static void TestLambdaExpressionComponents()
        {
            var builder = new ContainerBuilder();

            // 將實體註冊成元件(Component)，並將ILog和IConsole型別也註冊到
            var log = new ConsoleLog();
            builder.RegisterInstance(log)
                .As<ILog>()
                .As<IConsole>();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            var container = builder.Build();

            // 取得的ILog和IConsole為同一個元件(Component)
            var iLog = container.Resolve<ILog>();
            var iConsole = container.Resolve<IConsole>();
            iLog.Write($"ILog: {iLog.GetHashCode()}, IConsole: {iConsole.GetHashCode()}");
        }

        /// <summary>
        /// 藉由建構子的變數名稱，設定數值
        /// </summary>
        private static void TestNameParameter()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter("phoneNumber", "+123456789");

            var container = builder.Build();
            var iLog = container.Resolve<ILog>();
            iLog.Write("Named Parameter");
        }

        private static void TestNameParameter2()
        {
            const string phoneNumber = "phoneNumber";
            var builder = new ContainerBuilder();
            builder.Register((c, p) => new SMSLog(p.Named<string>(phoneNumber)))
                .As<ILog>();

            var random = new Random();
            var container = builder.Build();
            var iLog = container.Resolve<ILog>(new NamedParameter(phoneNumber, random.Next().ToString()));
            iLog.Write("NamedParameter 2");
        }

        /// <summary>
        /// 藉由建構子的類別種類，設定數值
        /// </summary>
        private static void TestTypeParameter()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter(new TypedParameter(typeof(string), "+12345678"));

            var container = builder.Build();
            var iLog = container.Resolve<ILog>();
            iLog.Write("Type Parameter");
        }

        /// <summary>
        /// 藉由建構子的變數名稱，設定數值
        /// </summary>
        private static void TestResolvedParameter()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SMSLog>()
                .As<ILog>()
                .WithParameter(
                    new ResolvedParameter(
                        // predicate
                        (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "phoneNumber",
                        // value accessor
                        (pi, ctx) => "+12345678"
                ));

            var container = builder.Build();
            var iLog = container.Resolve<ILog>();
            iLog.Write("Resolved Parameter");
        }



    }
}
