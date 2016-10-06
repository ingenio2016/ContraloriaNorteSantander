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
    public class AuditoriasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult InformeAuditorias()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Auditorias(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.Auditorias.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(auditoria.ToList());
        }

        public ActionResult AuditoriasInternas(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.AuditoriaInternas.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(auditoria.ToList());
        }

        public ActionResult AuditoriasExternas(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var auditoria = db.AuditoriaExternas.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(auditoria.ToList());
        }


        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var auditoria = db.Auditorias.Where(x => x.CompanyId == user.CompanyId);
            return View(auditoria.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
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
            var auditoria = new Auditoria { CompanyId = user.CompanyId, };
            return View(auditoria);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                db.Auditorias.Add(auditoria);
                try
                {
                    db.SaveChanges();
                    if (auditoria.AdjuntoFile != null)
                    {
                        var folder = "~/Content/Auditorias";
                        var file = string.Format("{0}_{1}", auditoria.AuditoriaId, auditoria.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(auditoria.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            auditoria.Adjunto = pic;
                            db.Entry(auditoria).State = EntityState.Modified;
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
                            "Name"); return View(auditoria);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                            CombosHelper.GetYears(),
                            "YearId",
                            "Name"); return View(auditoria);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Auditoria auditoria)
        {
            if (ModelState.IsValid)
            {
                if (auditoria.AdjuntoFile != null)
                {
                    var folder = "~/Content/Auditorias";
                    var file = string.Format("{0}_{1}", auditoria.AuditoriaId, auditoria.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(auditoria.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        auditoria.Adjunto = pic;
                        db.Entry(auditoria).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(auditoria).State = EntityState.Modified;
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
            return View(auditoria);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auditoria auditoria = db.Auditorias.Find(id);
            if (auditoria == null)
            {
                return HttpNotFound();
            }
            return View(auditoria);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auditoria auditoria = db.Auditorias.Find(id);
            db.Auditorias.Remove(auditoria);
            try
            {
                var response = FilesHelper.DeleteDocument(auditoria.Adjunto);
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
