using DummyGraphQLDapperSolution.Repository;
using GraphQL.Types;

namespace DummyGraphQLDapperSolution.GraphQL
{
    // 所有的GraphQL Queries都繼承至ObjectGraphType，GraphQL API端點之傳回值型別
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(CoilScheduleRepo repository)
        {           
            Field<ListGraphType<CoilScheduleType>>(
               "coilSchedule",
               resolve: context => repository.GetAll()
           );
        }
    }
}
