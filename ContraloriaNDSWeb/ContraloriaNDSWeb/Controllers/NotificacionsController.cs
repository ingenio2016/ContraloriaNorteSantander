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
    public class NotificacionsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult Notificaciones()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Notificacion(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var notificacion = db.Notificacions.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(notificacion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var notificacion = db.Notificacions.Where(x => x.CompanyId == user.CompanyId);
            return View(notificacion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacions.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.YearId = new SelectList(
               CombosHelper.GetYears(),
               "YearId",
               "Name");
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var notificacion = new Notificacion { CompanyId = user.CompanyId, };
            return View(notificacion);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                notificacion.Date = Convert.ToDateTime(fecha);
                notificacion.Autor = autor.FullName;
                notificacion.DateEdition = Convert.ToDateTime(fecha);
                notificacion.AutorEdition = autor.FullName;
                db.Notificacions.Add(notificacion);
                try
                {
                    db.SaveChanges();
                    if (notificacion.AdjuntoFile != null)
                    {
                        var folder = "~/Content/Notificaciones";
                        var file = string.Format("{0}_{1}", notificacion.NotificacionId, notificacion.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(notificacion.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            notificacion.Adjunto = pic;
                            db.Entry(notificacion).State = EntityState.Modified;
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

            ViewBag.YearId = new SelectList(
                           CombosHelper.GetYears(),
                           "YearId",
                           "Name");
            return View(notificacion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacions.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                                       CombosHelper.GetYears(),
                                       "YearId",
                                       "Name");
            return View(notificacion);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                if (notificacion.AdjuntoFile != null)
                {
                    var folder = "~/Content/Notificaciones";
                    var file = string.Format("{0}_{1}", notificacion.NotificacionId, notificacion.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(notificacion.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        notificacion.Adjunto = pic;
                        db.Entry(notificacion).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                notificacion.DateEdition = Convert.ToDateTime(fecha);
                notificacion.AutorEdition = autor.FullName;
                db.Entry(notificacion).State = EntityState.Modified;
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
                        ModelState.AddModelError(string.Empty, "Hay varios registros con el mismo valor");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.YearId = new SelectList(
                                                   CombosHelper.GetYears(),
                                                   "YearId",
                                                   "Name");
            return View(notificacion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacions.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notificacion notificacion = db.Notificacions.Find(id);
            db.Notificacions.Remove(notificacion);
            try
            {
                var response = FilesHelper.DeleteDocument(notificacion.Adjunto);
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
