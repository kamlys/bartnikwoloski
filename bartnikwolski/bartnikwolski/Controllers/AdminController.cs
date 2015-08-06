using bartnikwolski.Models;
using bartnikwolski.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bartnikwolski.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        BeekeeperDbContext db = new BeekeeperDbContext();

        public ActionResult Index()
        {
            AdminViewModel.Index model = new AdminViewModel.Index();
            model.PageList = db.Pages.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Page temp = db.Pages.First(p => p.PageId == id);
            AdminViewModel.Edit model = new AdminViewModel.Edit
            {
                PageId = temp.PageId,
                Content = temp.Content,
                Title = temp.Title,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AdminViewModel.Edit model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString();
                string filePath = fileName + Path.GetExtension(model.Picture.FileName);
                string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
                model.Picture.SaveAs(path);
                var tempPage = db.Pages.First(p => p.PageId == model.PageId);
                //if (model.Picture != null && tempPage.PictureSource != null)
                //{
                //    string delpath = Request.MapPath("~/Content/Photos/" + tempPage.PictureSource);
                //    if (System.IO.File.Exists(delpath))
                //        System.IO.File.Delete(delpath);
                //}
                tempPage.Content = model.Content;
                tempPage.Title = model.Title;
                tempPage.PictureSource = path;
                db.SaveChanges();
            }
            return View();
        }
    }
}