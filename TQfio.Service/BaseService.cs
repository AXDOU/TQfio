using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TQifo.IService;
using TQifo.Library;
using TQifo.Library.AttributeExtend;
using TQifo.Service;

namespace TQifo.Service
{
    public class BaseService : IBaseService
    {
        public bool Add<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var props = type.GetProperties().Where(p => p.Name != "Id");
            var parameters = props.Select(p => new SqlParameter($"@{p.GetMappingColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string sql = $"{TSqlHelper<T>.addSql}";
            return Command(sql, sqlcommand =>
            {
                sqlcommand.Parameters.AddRange(parameters);
                int result = sqlcommand.ExecuteNonQuery();
                if (result == 0)
                    throw new Exception("新增异常");
                return true;
            });
        }

        public bool Delete<T>(int id) where T : BaseModel
        {
            if (id == 0) throw new Exception("删除数据不存在");
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T>.deleteSql} WHERE Id={id}";
            return Command(sql, sqlcommand =>
            {
                int result = sqlcommand.ExecuteNonQuery();
                if (result == 0)
                    throw new Exception("删除异常");
                return true;
            });
        }

        public T FindT<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T>.findSql} where Id={id} ";
            List<T> list = ExecuteReader<T>(sql);
            return list.FirstOrDefault();
        }
        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T>.findSql}";
            return ExecuteReader<T>(sql);
        }


        private List<T> ExecuteReader<T>(string sql) where T:BaseModel
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            return Command<List<T>>(sql, SqlCommand =>
            {
                SqlDataReader reader = SqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach(PropertyInfo prop in type.GetProperties())
                    {
                        prop.SetValue(t, reader[prop.GetMappingColumnName()] == DBNull.Value ? null : reader[prop.GetMappingColumnName()]);
                    }
                    list.Add(t);
                }
                return list;
            });
        }

        /// <summary>
        /// 通用动作 创建 打开 释放连接 Sqlcommand创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private T Command<T>(string sql,Func<SqlCommand,T> func)
        {
            using(SqlConnection conn = new SqlConnection(StaticField.Connection))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                conn.Open();
                return func.Invoke(sqlCommand);
            }
        }

        public bool Update<T>(T t) where T : BaseModel
        {
            if (t.Id == 0) throw new Exception("修改数据不存在");
            Type type = typeof(T);
            var props = type.GetProperties().Where(p => !p.Name.Equals(t.Id));
            string sql = $"{TSqlHelper<T>.editSql} WHERE Id={t.Id}";
            var parameters = props.Select(p => new SqlParameter($"@{p.GetMappingColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray(); ;
            return Command(sql, SqlCommand =>
            {
                SqlCommand.Parameters.AddRange(parameters);
                int resulr = SqlCommand.ExecuteNonQuery();
                if (resulr == 0) throw new Exception("修改异常");
                return true;
            });
        }
    }
}
