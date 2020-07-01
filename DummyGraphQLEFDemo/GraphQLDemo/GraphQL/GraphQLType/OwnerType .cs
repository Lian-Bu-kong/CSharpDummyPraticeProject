using GraphQL.Types;
using GraphQLDemo.Entities;

namespace GraphQLDemo.GraphQL
{
    // 所有的GraphQL Types都繼承至ObjectGraphType<T>，並在泛型中指定Data Entity型別
    // 未來GraphQL API端點回傳的是GrqphQL Types [OwnerType]而非Entity Class [Owner]
    public class OwnerType : ObjectGraphType<Owner>
    {
        // 在建構式ObjectGraphType.Field方法指定GraphQL Types之欄位(成員)
        public OwnerType()
        {
            // Id欄位，對應於[Owner.Id]，Description指定欄位描述文字
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            // Name欄位，對應於[Owner.Name]
            Field(x => x.Name).Description("Name property from the owner object.");
            // Address欄位，對應於[Owner.Address]
            Field(x => x.Address).Description("Address property from the owner object.");
        }
    }
}
