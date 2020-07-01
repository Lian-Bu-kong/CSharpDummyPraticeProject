namespace DummyGraphQLDapperSolution.Repository
{
    public class CoilScheduleRepo : BaseRepository<DBModel.L3L2_Production_Schedule>
    {
        public CoilScheduleRepo(string connStr) : base(connStr)
        {
        }

        protected override string TableName => nameof(DBModel.L3L2_Production_Schedule);

        protected override string[] PKName => new string[] { nameof(DBModel.L3L2_Production_Schedule.Coil_ID) };

       
    }
}
