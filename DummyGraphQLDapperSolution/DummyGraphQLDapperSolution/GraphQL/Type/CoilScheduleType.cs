using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DummyGraphQLDapperSolution.Repository.DBModel;

namespace DummyGraphQLDapperSolution.GraphQL
{
    public class CoilScheduleType : ObjectGraphType<L3L2_Production_Schedule>
    {
        public CoilScheduleType()
        {
            // Coil_ID欄位，對應於[L3L2_Production_Schedule.Coil_ID欄位]
            Field(x => x.Coil_ID).Description("coilID property from the owner object.");
            // Seq_No欄位，對應於[L3L2_Production_Schedule.Seq_No欄位]
            Field(x => x.Seq_No, type: typeof(IntGraphType)).Description("seqNo property from the owner object.");
        }
    }
}
