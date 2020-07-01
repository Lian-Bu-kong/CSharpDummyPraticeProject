using GraphQL.Types;
using GraphQLDemo.Contracts;

namespace GraphQLDemo.GraphQL.GraphQLQueries
{
    // 所有的GraphQL Queries都繼承至ObjectGraphType，這是GraphQL API端點之傳回值型別
    public class AppQuery : ObjectGraphType
    {
        // 在建構式以DI Pattern 引入Repository物件以進行實際的資料存取作業
        public AppQuery(IOwnerRepository repository)
        {
            // 以泛型版Field方法指定傳回值型別，ListGraphType是GraphQL.Net中的List<T>，
            // ListGraphType<OwnerType>就表示C#中的List<Owner>，
            // Field的第1個參數”owners”為此欄位的名稱，未來Client端必須以此指定回傳之欄位，
            // 第2個參數會呼叫Repository類別之資料存取方法，並回傳真正的結果。
            Field<ListGraphType<OwnerType>>(
               "owners",
               resolve: context => repository.GetAll()
           );
        }
    }
}
