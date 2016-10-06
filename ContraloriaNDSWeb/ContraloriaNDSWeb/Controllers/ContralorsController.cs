using System;
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
    public class ContralorsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult NuestroContralor()
        {
            var contralors = db.Contralors.Include(c => c.Company);
            return View(contralors.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contralor = db.Contralors.Where(x => x.CompanyId == user.CompanyId);
            return View(contralor.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contralor contralor = db.Contralors.Find(id);
            if (contralor == null)
            {
                return HttpNotFound();
            }
            return View(contralor);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contralor = new Contralor { CompanyId = user.CompanyId, };
            return View(contralor);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contralor contralor)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                contralor.Date = Convert.ToDateTime(fecha);
                contralor.Autor = autor.FullName;
                contralor.DateEdition = Convert.ToDateTime(fecha);
                contralor.AutorEdition = autor.FullName;
                db.Contralors.Add(contralor);
                try
                {
                    db.SaveChanges();
                    if (contralor.PhotoFile != null)
                    {
                        var folder = "~/Content/Contralor";
                        var file = string.Format("{0}.jpg", contralor.ContralorId);
                        var response = FilesHelper.UploadPhoto(contralor.PhotoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            contralor.Photo = pic;
                            db.Entry(contralor).State = EntityState.Modified;
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
                        ModelState.AddModelError(string.Empty, "There are a record with the same value");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", contralor.CompanyId);
            return View(contralor);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contralor contralor = db.Contralors.Find(id);
            if (contralor == null)
            {
                return HttpNotFound();
            }
            return View(contralor);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contralor contralor)
        {
            if (ModelState.IsValid)
            {
                if (contralor.PhotoFile != null)
                {
                    var folder = "~/Content/Contralor";
                    var file = string.Format("{0}.jpg", contralor.ContralorId);
                    var response = FilesHelper.UploadPhoto(contralor.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        contralor.Photo = pic;
                        db.Entry(contralor).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
               
                contralor.DateEdition = Convert.ToDateTime(fecha);
                contralor.AutorEdition = autor.FullName;
                db.Entry(contralor).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                                                                                    ex.InnerException.InnerException != null &&
                                                                                                    ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same value");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                    }
                }
                return RedirectToAction("Index");
            }
            return View(contralor);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contralor contralor = db.Contralors.Find(id);
            if (contralor == null)
            {
                return HttpNotFound();
            }
            return View(contralor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contralor contralor = db.Contralors.Find(id);
            db.Contralors.Remove(contralor);
            try
            {
                var response = FilesHelper.DeleteDocument(contralor.Photo);
                db.SaveChanges();
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
