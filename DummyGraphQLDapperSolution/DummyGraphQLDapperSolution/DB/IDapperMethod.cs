using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DummyGraphQLDapperSolution.DB
{
    public interface IDapperMethod
    {
        int Execute(string sql, object data);
        int Execute(string sql);
        T ExecuteScalar<T>(string sql);
        IEnumerable<T> Query<T>(string sql);
        T QueryFirstOrDefault<T>(string sql);
        T QuerySingleOrDefault<T>(string sql);
        void SqlBulkCopy(DataTable dt, string tableName);
    }
}
