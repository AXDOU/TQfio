using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TQifo.Library;
using TQifo.Library.AttributeExtend;

namespace TQifo.Service
{
    public static class TSqlHelper<T> where T : BaseModel
    {

        public static string findSql = null, addSql = null, deleteSql = null, editSql = null;
        public static Type type = typeof(T);
        public static string tableName = AttributeHelper.GetMappingName<T>();

       static TSqlHelper()
        {
            string columnName = string.Join(",", type.GetProperties().Select(prop => $"[{prop.GetMappingColumnName()}]"));
            findSql = $"SELECT {columnName} from [{tableName}]";

            var props = type.GetProperties().Where(p => !p.Name.Equals("Id"));
            var columns = string.Join(",", props.Select(prop => $"[{prop.GetMappingColumnName()}]=@{prop.GetMappingColumnName()}"));
            editSql = $"UPDATE [{tableName}] SET {columns}";
            deleteSql = $"DELETE FROM [{tableName}]";

            columnName = string.Join(",", props.Select(prop => $"[{prop.GetMappingColumnName()}]"));
            string columnValue = string.Join(",", props.Select(prop => $"@{ prop.GetMappingColumnName()}"));
            addSql = $"INSERT INTO [{tableName}]({columnName}) VALUES({columnValue})";
        }
    }
}
