using bartnikwolski.Models;
using bartnikwolski.ViewModels;
using bartnikwolski.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace bartnikwolski.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        BeekeeperDbContext db = new BeekeeperDbContext();

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.PageList = db.Pages.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult EditPage(string title)
        {
            Page temp = db.Pages.First(p => p.Title == title);
            EditViewModel model = new EditViewModel
            {
                Content = temp.Content,
                Title = temp.Title,
                PictureSource = temp.PictureSource,
                HasItems = temp.HasProducts
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPage(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tempPage = db.Pages.First(p => p.Title == model.Title);
                if (model.Picture != null && tempPage.PictureSource != null)
                {
                    string oldpath = Request.MapPath("~/Content/Photos/" + tempPage.PictureSource);
                    if (System.IO.File.Exists(oldpath))
                        System.IO.File.Delete(oldpath);
                }
                string fileName = Guid.NewGuid().ToString();
                string filePath = fileName + Path.GetExtension(model.Picture.FileName);
                string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
                model.Picture.SaveAs(path);
                tempPage.PictureSource = filePath;
                tempPage.Content = model.Content;
                tempPage.Title = model.Title;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }

        public JsonResult GetProducts()
        {
            var allproducts = db.Products.Select(p => new
            {
                ProductId = p.ProductId,
                Description = p.Description,
                Name = p.Name,
                PictureSource = p.PictureSource,
                IsSpecial = p.IsSpecial,
                IsProduct = p.IsProduct
            }).AsEnumerable();
            return Json(allproducts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateItem(CreateItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString();
                string filePath = fileName + Path.GetExtension(model.PictureSource.FileName);
                string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
                model.PictureSource.SaveAs(path);
                db.Products.Add(new Product
                {
                    Description = model.Description,
                    Name = model.Name,
                    IsProduct = model.IsProduct,
                    IsSpecial = model.IsSpecial,
                    PictureSource = "Content/Photos/" + filePath
                });
                db.SaveChanges();
                Response.Redirect("Products");
            }
            return View(model);
        }

        public JsonResult Delete(int id)
        {
            Product product = db.Products.First(p => p.ProductId == id);
            string path = Request.MapPath("~/" + product.PictureSource);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            db.Products.Remove(product);
            db.SaveChanges();
            return null;
        }

        [HttpGet]
        public ActionResult AccountSettings()
        {
            AccountSettingsViewModel model = new AccountSettingsViewModel();
            model.Login = User.Identity.Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult AccountSettings(AccountSettingsViewModel model)
        {
            var thisuser = db.Users.First(u => u.Login == User.Identity.Name);
            if (ModelState.IsValid && Crypto.VerifyHashedPassword(thisuser.Password, model.OldPassword))
            {
                thisuser.Password = Crypto.HashPassword(model.Password);
                db.SaveChanges();
            }
            return View();
        }

        public JsonResult SaveChanges(Product product)//int id, string title, string name, string description)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return null;
        }

        public JsonResult DeleteFile(string source)
        {
            string path = Request.MapPath("~/" + source);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            return null;
        }

        public JsonResult SaveFile(HttpPostedFileBase file)
        {
            string fileName = Guid.NewGuid().ToString();
            string filePath = fileName + Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath(@"~/Content/Photos/"), filePath);
            file.SaveAs(path);
            return Json("Content/Photos/" + filePath, JsonRequestBehavior.AllowGet);
        }
    }
}