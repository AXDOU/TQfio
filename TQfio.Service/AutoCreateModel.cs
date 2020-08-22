using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Xml.Linq;
using TQifo.Library;

namespace TQifo.Service
{
    public static  class AutoCreateModel
    {
        private static string getAllTableSql = "SELECT name FROM sys.tables where type ='U'";
        private static string getTableInfoSql = @"SELECT DISTINCT  
                                                  A.COLUMN_NAME colname,
                                                  A.DATA_TYPE typename,
                                                  A.IS_NULLABLE isnullable FROM INFORMATION_SCHEMA.Columns A LEFT JOIN 
                                                  INFORMATION_SCHEMA.KEY_COLUMN_USAGE B ON A.TABLE_NAME=B.TABLE_NAME";
    
        public static void BatchMappingModel()
        {
            using(SqlConnection conn =new SqlConnection(StaticField.Connection))
            {
                SqlCommand sqlCommand = new SqlCommand(getAllTableSql, conn);
                conn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    MappingModel(reader["name"].ToString());
                }
            }
        }

        private static void MappingModel(string tableName)
        {
            string sql = $"{getTableInfoSql} where a.table_name ='{tableName}'";
            using(SqlConnection conn=new SqlConnection(StaticField.Connection))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"public class {tableName} \r\n {{\r\n}}");
                while (reader.Read())
                {
                    stringBuilder.Append($"public {GetTypeOfColumn(reader["typename"].ToString(), reader["isnullable"].ToString())} {reader["colname"].ToString()} {{get;set;}}\r\n");
                }
                stringBuilder.Append("}");
                CommonMethod.CreateTxt(stringBuilder.ToString(), StaticField.ModelFilePath + "\\" + tableName + ".txt");
            }
        }

        private static string GetTypeOfColumn(string type,string nullAble)
        {
            if (type.Equals("int") && nullAble.Equals("NO"))
                return "int";
            else if (type.Equals("int") && nullAble.Equals("YES"))
                return "int?";
            else if (type.Equals("datetime") && nullAble.Equals("YES"))
                return "DateTime?";
            else if (type.Equals("datetime") && nullAble.Equals("NO"))
                return "DateTime";
            else if (type.Equals("nvarchar") || type.Equals("varchar") || type.Equals("text"))
                return "string";
            else if (type.Contains("decimal"))
                return "decimal";
            else if (type.Equals("float"))
                return "double";
            else
                throw new Exception("暂不支持");
        }
    }
}
