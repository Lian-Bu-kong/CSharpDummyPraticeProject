using System;
using System.Text;
using static DummyGraphQLDapperSolution.DB.DBAttributes;

namespace DummyGraphQLDapperSolution.DB.sqlGen
{
    public class DapperSqlGen : ISqlGenStrategy
    {
        private readonly int ExcuteFail = -1;

        #region -- CURD --

        public string Create(string table, object data)
        {
            int infNum = ExcuteFail;
            var sql = new StringBuilder();
            sql.Append("INSERT");
            sql.Append(" INTO ");
            sql.Append(table);
            sql.Append("(");

            if (data != null)
            {
                // Identity key
                string prefix = "";
                foreach (var field in data.GetType().GetProperties())
                {
                    //For 測試暫寫-正規操作下插入新資料盡量避免有欄位是有NULL狀況
                    if (field.GetValue(data) == null)
                        continue;

                    if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
                        continue;

                    sql.Append(prefix);
                    prefix = ",";
                    sql.Append(field.Name);

                };
                sql.Append(")");


                // Identity key value
                sql.Append(" VALUES(");
                prefix = "";
                foreach (var field in data.GetType().GetProperties())
                {
                    //For 測試暫寫-正規操作下插入新資料盡量避免有欄位是有NULL狀況
                    if (field.GetValue(data) == null)
                        continue;

                    if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
                        continue;

                    sql.Append(prefix);
                    prefix = ",";
                    sql.Append(SqlGenHelp.GetInsertValue(field.GetValue(data)));
                };

            }
            else
            {
                sql.Append("VALUES (NULL");
            }

            sql.Append(")");
            //Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        public string Update(string table, object updateData, string condition)
        {
            var sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(table);
            sql.Append(" SET ");

            // SET
            string prefix = "";
            foreach (var field in updateData.GetType().GetProperties())
            {

                if (field.GetValue(updateData) == null)
                    continue;

                if (Attribute.IsDefined(field, typeof(IgnoreReflction)))
                    continue;

                sql.Append(prefix);
                prefix = ",";

                sql.Append(field.Name);
                sql.Append("=");

                var parmName = "@" + field.Name;
                sql.Append(parmName);

            };

            // WHERE
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(" WHERE ");
                sql.Append(condition);
            }

            Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        public string Read(string table, bool isReadAll, int num = 1, string condition = "", string otherCondition = "")
        {
            var sql = new StringBuilder();

            if (isReadAll)
                sql.Append("SELECT * FROM ");
            else
                sql.Append($"SELECT TOP {num} * FROM ");

            sql.Append(table);
            // WHERE的條件
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(" WHERE ");
                sql.Append(condition);
            }
            // 其餘條件
            if (!string.IsNullOrEmpty(otherCondition))
            {
                sql.Append(string.Format(" {0}", otherCondition));
            }
            //Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        public string Delete(string table, string condition)
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM ");
            sql.Append(table);

            if (!condition.Equals(""))
            {
                sql.Append(" WHERE ");
                sql.Append(condition);
            }

            Console.WriteLine(sql.ToString());
            return sql.ToString();
        }

        #endregion
    }
}
