using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TQifo.Library.AttributeExtend;

namespace TQifo.Library
{
    public class CommonMethod
    {
        private static string LogPath = AppDomain.CurrentDomain.BaseDirectory;

        public static void ShowModelInfo<T>(T t)
        {
            Type type = t.GetType();
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($"属性{prop.Name}的值 {prop.GetValue(t)}");
            }

            foreach(var prop in type.GetProperties())
            {
                Console.WriteLine($"属性{prop.GetColumnName()}:{prop.GetValue(t)})");
            }
        }

        public static void CreateTxt(string content, string path = "")
        {
            StreamWriter sw = null;
            try
            {
                string fileName = $"log{DateTime.Now.ToString("yyyyMMdd")}.txt";
                string totalPath = string.IsNullOrEmpty(path) ? Path.Combine(LogPath, fileName):path;
                if (Directory.Exists(LogPath))
                {
                    sw = File.Exists(totalPath) ? File.AppendText(totalPath) : File.CreateText(totalPath);
                }
                else
                {
                    Directory.CreateDirectory(LogPath);
                    sw = File.CreateText(totalPath);
                }
                sw.Write(content);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"{typeof(CommonMethod).Name}类中CretaeTxt方法异常",ex);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        public static void WriteLog(string msg)
        {
            var txt = $"时间：{DateTime.Now}\r\n 异常错误：{msg}";
            CreateTxt(txt);
        }

        public static void TryCatchAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch(Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
    }
}
