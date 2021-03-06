﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContraloriaNDSWeb.Models;
using ContraloriaNDSWeb.Classes;

namespace ContraloriaNDSWeb.Controllers
{
    public class UsersController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.Company).Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(),
                "CityId",
                "Name");

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(),
                "CompanyId",
                "Name");

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(),
                "DepartmentId",
                "Name");

            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                user.Date = Convert.ToDateTime(fecha);
                user.Autor = autor.FullName;
                user.DateEdition = Convert.ToDateTime(fecha);
                user.AutorEdition = autor.FullName;
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                    UsersHelper.CreateUserASP(user.UserName, "User");
                    if (user.PhotoFile != null)
                    {
                        var folder = "~/Content/Users";
                        var file = string.Format("{0}.jpg", user.UserId);
                        var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            user.Photo = pic;
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                                                                                       ex.InnerException.InnerException != null &&
                                                                                                                       ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay varios registros con el mismo valor");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(
                CombosHelper.GetCities(),
                "CityId",
                "Name",
                user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(),
                "CompanyId",
                "Name",
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(),
                "DepartmentId",
                "Name",
                user.DepartmentId);

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(
               CombosHelper.GetCities(),
               "CityId",
               "Name",
               user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(),
                "CompanyId",
                "Name",
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(),
                "DepartmentId",
                "Name",
                user.DepartmentId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Users";
                    var file = string.Format("{0}.jpg", user.UserId);
                    var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        user.Photo = pic;
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                var db2 = new ContraloriandsContext();
                var currentUser = db2.Users.Find(user.UserId);

                if (currentUser.UserName != user.UserName)
                {
                    UsersHelper.UpdateUserName(currentUser.UserName, user.UserName);
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                user.Date = Convert.ToDateTime(fecha);
                user.Autor = autor.FullName;
                user.DateEdition = Convert.ToDateTime(fecha);
                user.AutorEdition = autor.FullName;
                db.Entry(user).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                                                                                        ex.InnerException.InnerException != null &&
                                                                                                                        ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Hay varios registros con el mismo valor");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                    }
                }
            }
            ViewBag.CityId = new SelectList(
               CombosHelper.GetCities(),
               "CityId",
               "Name",
               user.CityId);

            ViewBag.CompanyId = new SelectList(
                CombosHelper.GetCompanies(),
                "CompanyId",
                "Name",
                user.CompanyId);

            ViewBag.DepartmentId = new SelectList(
                CombosHelper.GetDepartments(),
                "DepartmentId",
                "Name",
                user.DepartmentId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            try
            {
                var response = FilesHelper.DeleteDocument(user.Photo);
                db.SaveChanges();
                UsersHelper.DeleteUser(user.UserName);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                                                                                    ex.InnerException.InnerException != null &&
                                                                                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "El registro no se puede eliminar porque tiene registros relacionados");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.ToString());
                }
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetCities(int departmentId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(m => m.DepartmentId == departmentId);
            return Json(cities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
