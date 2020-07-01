using GraphQL;
using GraphQL.Types;


namespace DummyGraphQLDapperSolution.GraphQL
{
    public class AppSchema : Schema
    {
        // 在建構式引入IDependencyResolver類別，以解析/處理Client Request事件
        public AppSchema(IDependencyResolver resolver)
            : base(resolver)
        {          
            Query = resolver.Resolve<AppQuery>();
        }
    }
}
