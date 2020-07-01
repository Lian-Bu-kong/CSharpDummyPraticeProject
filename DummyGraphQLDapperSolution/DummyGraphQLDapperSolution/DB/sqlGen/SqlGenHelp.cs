using System;
using System.Collections.Generic;

/*
 *Author: ICSC SPYUA
 *Date:2020/6/3
 *Desc:Sql產生解析Help
 */

namespace DummyGraphQLDapperSolution.DB.sqlGen
{
    public class SqlGenHelp
    {
        /// <summary>
        /// Get the insert format command string of the KeyValuePair value of the property
        /// </summary>
        public static string GetInsertValue(object itemValue)
        {
            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string): return string.Format("'{0}'", itemValue);
                case Type dateType when dateType == typeof(DateTime): return string.Format("'{0}'", ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));
                default: return string.Format("{0}", itemValue);
            }
        }

        /// <summary>
        /// Get the update format command string of the KeyValuePair value of the property
        /// </summary>
        public static string GetUpdateValue(string itemKey, object itemValue)
        {
            switch (itemValue.GetType())
            {
                case Type strType when strType == typeof(string): return string.Format("{0}='{1}'", itemKey, itemValue);
                case Type dateType when dateType == typeof(DateTime): return string.Format("{0}='{1}'", itemKey, ((DateTime)itemValue).ToString("yyyy/MM/dd HH:mm:ss.fff"));
                default: return string.Format("{0}={1}", itemKey, itemValue);
            }
        }

        /// <summary>
        /// Get sql condition string
        /// </summary>
        public static string GetSqlWhere(Dictionary<string, object> dicKey)
        {
            string strWhere = "";

            try
            {
                if (dicKey != null && dicKey.Count > 0)
                {
                    strWhere = "Where ";

                    foreach (KeyValuePair<string, object> item in dicKey)
                    {
                        string condition = (item.Value.GetType().Name == "String") ? "{0}='{1}' And " : "{0}={1} And ";
                        strWhere += string.Format(condition, item.Key, item.Value);
                    }

                    strWhere = strWhere.Substring(0, strWhere.Length - 4);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //_logger.Error("GetSqlWhere. ex=" + ex.ToString());
                //_logger.Error(" ex.Message=" + ex.Message);
                //_logger.Error(" ex.StackTrace=" + ex.StackTrace);
            }

            return strWhere;
        }
    }
}
