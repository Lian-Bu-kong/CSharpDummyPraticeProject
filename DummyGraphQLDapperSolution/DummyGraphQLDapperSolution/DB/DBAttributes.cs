using System;

namespace DummyGraphQLDapperSolution.DB
{
    public class DBAttributes
    {
        public class PrimaryKey : Attribute
        {
            public PrimaryKey() { }
        }
        public class IdentityKey : Attribute
        {
            public IdentityKey() { }
        }

        [AttributeUsage(AttributeTargets.All, Inherited = false)]
        public class IgnoreReflction : Attribute
        {
            public IgnoreReflction() { }
        }
    }
}
