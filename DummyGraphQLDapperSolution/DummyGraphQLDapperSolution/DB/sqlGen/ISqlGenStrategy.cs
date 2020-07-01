/*
 *Author: ICSC SPYUA
 *Date:2020/6/3
 *Desc:Sql CURD Interface
 */

namespace DummyGraphQLDapperSolution.DB.sqlGen
{
    public interface ISqlGenStrategy
    {
        string Create(string table, object data);

        string Update(string table, object data, string condition);

        string Read(string table, bool isReadAll, int num = 1, string condition = "", string otherCondition = "");

        string Delete(string table, string condition);
    }
}
