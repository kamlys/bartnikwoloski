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

        public ActionResult Items()
        {
            var items = db.Products.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult CreateItem()
        {
            CreateItemViewModel model = new CreateItemViewModel();
            model.Pages = db.Pages.Where(p => p.HasProducts == true).Select(p => new SelectListItem { Text = p.Title });
            return View(model);
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
                    PageTitle = model.SelectedPageTitle,
                    PictureSource = "Content/Photos/" + filePath
                });
                db.SaveChanges();
                return RedirectToAction("Items");
            }
            return View(model);
        }

        public ActionResult DeleteItem(int id)
        {
            Product product = db.Products.First(p => p.ProductId == id);
            string path = Request.MapPath("~/" + product.PictureSource);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Items");
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
    }
}