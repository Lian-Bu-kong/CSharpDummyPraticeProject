using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyGraphQLDapperSolution.DB
{
    public interface IDBContext : IDapperMethod
    {
        int Create(string table, dynamic data);
        int Update(string table, object data, string condition);
        IEnumerable<T> Read<T>(string table, bool isReadAll, int num, string condition = "", string otherCondition = "");
        T ReadOne<T>(string table, string condition = "", string otherCondition = "");
        int Delete(string table, string condition);
    }
}
