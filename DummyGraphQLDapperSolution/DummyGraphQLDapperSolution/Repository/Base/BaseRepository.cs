using DummyGraphQLDapperSolution.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyGraphQLDapperSolution.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class, IRepositoryModel, new()    //約束型別
    {

        public IDBContext DBContext;

        /// <summary>
        /// 取得表格名稱
        /// </summary>
        protected abstract string TableName { get; }
        /// <summary>
        /// 取得PK名稱
        /// </summary>
        protected abstract string[] PKName { get; }

        public BaseRepository(string connStr)
        {
            DBContext = new DapperDBContext(connStr);
        }

        /// <summary>
        /// 插入一筆資料
        /// </summary>
        public virtual int Insert(T obj)
        {
            obj.CreateTime = DateTime.Now;
            obj.UpdateTime = DateTime.Now;
            //return DBContext.Create(TableName, UpdateToTableData(obj));
            return DBContext.Create(TableName, obj);
        }

        /// <summary>
        /// 獲取 一筆資料
        /// </summary>
        public virtual T Get(string[] pkValue)
        {
            var contentValues = DBContext.ReadOne<T>(TableName, SetPkWhereSql(pkValue));

            return contentValues;
        }

        /// <summary>
        /// 刪除單一筆資料，藉由id
        /// </summary>
        public virtual int Delete(string[] pkValue, string selfCondition = "")
        {
            return DBContext.Delete(
                        TableName,
                        selfCondition.Equals("") ? SetPkWhereSql(pkValue) : selfCondition
                    );
        }

        /// <summary>
        /// 更新一筆資料
        /// </summary>
        public virtual int Update(T obj, string[] pkValue = null)
        {
            obj.UpdateTime = DateTime.Now;
            obj.CreateTime = DateTime.Now;
            return DBContext.Update(TableName, obj, SetPkWhereSql(pkValue));
        }

        /// <summary>
        /// 獲取 全部資料
        /// </summary>
        public virtual IEnumerable<T> GetAll()
        {
            var contentValues = DBContext.Read<T>(TableName, true, -1);
            return contentValues;
        }

        public virtual IEnumerable<T> GetAll(int limitCount)
        {
            var contentValues = DBContext.Read<T>(TableName, false, limitCount);
            return contentValues;
        }

        public virtual IEnumerable<T> GetAll(string condition)
        {
            var contentValues = DBContext.Read<T>(TableName, true, -1, condition);
            return contentValues;
        }

        public virtual IEnumerable<T> GetAll(string condition, int limitCount)
        {
            var contentValues = DBContext.Read<T>(TableName, false, limitCount, condition);
            return contentValues;
        }

        public virtual IEnumerable<T> GetAllOrderBy(string orderCondition, bool isReadAll = true, int limitCount = 0)
        {
            var contentValues = DBContext.Read<T>(TableName, isReadAll, limitCount, "", orderCondition);
            return contentValues;
        }


        public virtual int GetTotalNum()
        {
            var sql = $"select count(*) FROM {TableName}";
            return DBContext.ExecuteScalar<int>(sql);
        }

        public virtual bool HasData(string[] pkValue)
        {
            var where = SetPkWhereSql(pkValue);
            var sql = $"select count(1) from {TableName} WHERE {where}";
            return DBContext.ExecuteScalar<bool>(sql);
        }

        private string SetPkWhereSql(string[] pkValue)
        {
            if (pkValue == null)
                return "";

            var sqlWhere = new StringBuilder();

            string prefix = "";
            for (var i = 0; i < pkValue.Length; i++)
            {
                sqlWhere.Append(prefix);
                prefix = " AND ";
                sqlWhere.Append($"{PKName[i]}='{pkValue[i]}'");
            }

            return sqlWhere.ToString();
        }

    }

}
