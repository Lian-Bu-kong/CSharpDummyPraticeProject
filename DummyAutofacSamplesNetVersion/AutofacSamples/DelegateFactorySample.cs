using Autofac;

namespace AutofacSamples
{
    public class Service
    {
        public string DoSomething(int vlaue)
        {
            return $"I have {vlaue}";
        }
    }

    public class DomainObject
    {
        private Service _service;
        private int _value;

        public delegate DomainObject Factory(int value);

        public DomainObject(Service service, int value)
        {
            _service = service;
            _value = value;
        }

        public override string ToString()
        {
            return _service.DoSomething(_value);
        }
    }


    public class DelegateFactorySample
    {
        public static void Demo()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service>();
            builder.RegisterType<DomainObject>();

            var container = builder.Build();
            var dominaObject1 = container.Resolve<DomainObject>(new PositionalParameter(1, 42));
            System.Console.WriteLine(dominaObject1);

            // 使用Facotry注入
            var factory = container.Resolve<DomainObject.Factory>();
            var dominaObject2 = factory(43);
            System.Console.WriteLine(dominaObject2);
        }
    }






}
