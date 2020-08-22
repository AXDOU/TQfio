using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TQifo.Factory;
using TQifo.IService;
using TQifo.Library;
using TQifo.Library.AttributeExtend;
using System.Configuration;
using TQifo.Model;
namespace TQifo.web
{
    public class HomeController : Controller
    {
       
        // GET: HomeController
        public ActionResult Index()
        {
            IBaseService baseService = SimpleFactory.CreateInstance();
            List<UserModel> userDatas = baseService.FindAll<UserModel>();

            var user = baseService.FindT<UserModel>(1);
            user.Name = "小新2";
            bool isUpdate = baseService.Update<UserModel>(user);
            UserModel userModel = new UserModel
            {
                Name = "太子爷2",
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
            if(!userModel.Validate<UserModel>(out List<string> errList))
            {

            }
            bool isAdd = baseService.Add<UserModel>(userModel);
            bool isDel = baseService.Delete<UserModel>(4);
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
