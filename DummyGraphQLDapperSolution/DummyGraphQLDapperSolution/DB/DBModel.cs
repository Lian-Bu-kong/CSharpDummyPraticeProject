using System;
using static DummyGraphQLDapperSolution.DB.DBAttributes;

namespace DummyGraphQLDapperSolution.Repository
{
    public class DBModel
    {
        public class L3L2_Production_Schedule : BaseRepositoryModel
        {
            [PrimaryKey]
            public string Coil_ID { get; set; }
            public short Seq_No { get; set; }
            public string Update_Source { get; set; } = "";
            public override DateTime UpdateTime { get; set; } = DateTime.Now;
        }
    }
}
