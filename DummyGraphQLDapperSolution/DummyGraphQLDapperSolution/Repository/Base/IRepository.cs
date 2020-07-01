using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyGraphQLDapperSolution.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// 插入一筆資料
        /// </summary>
        int Insert(T obj);

        /// <summary>
        /// 更新一筆資料
        /// </summary>
        int Update(T obj, string[] pkValue);

        /// <summary>
        /// 刪除單一筆資料，藉由id
        /// </summary>
        int Delete(string[] pkValue, string condition);

        /// <summary>
        /// 獲取 一筆資料
        /// </summary>
        T Get(string[] pkValue);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int limitCount);
        IEnumerable<T> GetAll(string condition);
        IEnumerable<T> GetAll(string condition, int limitCount);
        IEnumerable<T> GetAllOrderBy(string orderCondition, bool isReadAll = true, int limitCount = 0);
    }
}
