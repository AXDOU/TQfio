using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TQifo.Factory;
using TQifo.IService;
using TQifo.Model;
using TQifo.Service;
using TQifo.Library;
using TQifo.Library.AttributeExtend;
namespace TQifo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {


            IBaseService baseService2 = SimpleFactory.CreateInstance();
            BaseService baseService = new BaseService();
            List<UserModel> userDatas = baseService2.FindAll<UserModel>();

            var user = baseService2.FindT<UserModel>(1);
            user.Name = "Ð¡ÐÂ3";
            bool isUpdate = baseService2.Update<UserModel>(user);
            UserModel userModel = new UserModel
            {
                Name = "Ì«×ÓÒ¯2",
                Account = "100000",//"10000",
                Password = "12345",
                CompanyName = "58",
                CreateTime = DateTime.Now,
                CreatorId = 1,
                Mobile = "15910550576",
                Email = "767921337@qq.com",
                Status = 1,
                UserType = 1
            };
            if (!userModel.Validate<UserModel>(out List<string> errList))
            {

            }
            bool isAdd = baseService.Add<UserModel>(userModel);
            bool isDel = baseService.Delete<UserModel>(4);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
