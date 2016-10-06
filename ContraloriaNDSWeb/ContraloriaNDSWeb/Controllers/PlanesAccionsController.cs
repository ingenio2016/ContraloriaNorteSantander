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
    public class PlanesAccionsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult PlanesAccion(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var planesAccion = db.PlanesAccions.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (planesAccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(planesAccion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var PlanAccion = db.PlanesAccions.Where(x => x.CompanyId == user.CompanyId);
            return View(PlanAccion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesAccion planesAccion = db.PlanesAccions.Find(id);
            if (planesAccion == null)
            {
                return HttpNotFound();
            }
            return View(planesAccion);
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
            var planaccion = new PlanesAccion { CompanyId = user.CompanyId, };
            return View(planaccion);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanesAccion planesAccion)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                planesAccion.Date = Convert.ToDateTime(fecha);
                planesAccion.Autor = autor.FullName;
                planesAccion.DateEdition = Convert.ToDateTime(fecha);
                planesAccion.AutorEdition = autor.FullName;
                db.PlanesAccions.Add(planesAccion);
                try
                {
                    db.SaveChanges();
                    if (planesAccion.AdjuntoFile != null)
                    {
                        var folder = "~/Content/PlanAccionAdjuntos";
                        var file = string.Format("{0}_{1}", planesAccion.PlanesAccionId, planesAccion.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(planesAccion.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            planesAccion.Adjunto = pic;
                            db.Entry(planesAccion).State = EntityState.Modified;
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", planesAccion.CompanyId);
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", planesAccion.YearId);
            return View(planesAccion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesAccion planesAccion = db.PlanesAccions.Find(id);
            if (planesAccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                           CombosHelper.GetYears(),
                           "YearId",
                           "Name");
            return View(planesAccion);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanesAccion planesAccion)
        {
            if (ModelState.IsValid)
            {
                if (planesAccion.AdjuntoFile != null)
                {
                    var folder = "~/Content/PlanAccionAdjuntos";
                    var file = string.Format("{0}_{1}", planesAccion.PlanesAccionId, planesAccion.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(planesAccion.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        planesAccion.Adjunto = pic;
                        db.Entry(planesAccion).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
               
                planesAccion.DateEdition = Convert.ToDateTime(fecha);
                planesAccion.AutorEdition = autor.FullName;
                db.Entry(planesAccion).State = EntityState.Modified;
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
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", planesAccion.YearId);
            return View(planesAccion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesAccion planesAccion = db.PlanesAccions.Find(id);
            if (planesAccion == null)
            {
                return HttpNotFound();
            }
            return View(planesAccion);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanesAccion planesAccion = db.PlanesAccions.Find(id);
            db.PlanesAccions.Remove(planesAccion);
            try
            {
                var response = FilesHelper.DeleteDocument(planesAccion.Adjunto);
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
