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
    [Authorize(Roles = "User")]
    public class AuditoriaInternasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: AuditoriaInternas
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var auditoria = db.AuditoriaInternas.Where(x => x.CompanyId == user.CompanyId);
            return View(auditoria.ToList());
        }

        // GET: AuditoriaInternas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaInterna auditoriaInterna = db.AuditoriaInternas.Find(id);
            if (auditoriaInterna == null)
            {
                return HttpNotFound();
            }
            return View(auditoriaInterna);
        }

        // GET: AuditoriaInternas/Create
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
            var auditoria = new AuditoriaInterna { CompanyId = user.CompanyId, };
            return View(auditoria);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditoriaInterna auditoriaInterna)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                auditoriaInterna.Date = Convert.ToDateTime(fecha);
                auditoriaInterna.Autor = autor.FullName;
                auditoriaInterna.DateEdition = Convert.ToDateTime(fecha);
                auditoriaInterna.AutorEdition = autor.FullName;
                db.AuditoriaInternas.Add(auditoriaInterna);
                try
                {
                    db.SaveChanges();
                    if (auditoriaInterna.AdjuntoFile != null)
                    {
                        var folder = "~/Content/AuditoriasInternas";
                        var file = string.Format("{0}_{1}", auditoriaInterna.AuditoriaInternaId, auditoriaInterna.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(auditoriaInterna.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            auditoriaInterna.Adjunto = pic;
                            db.Entry(auditoriaInterna).State = EntityState.Modified;
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
            return View(auditoriaInterna);
        }

        // GET: AuditoriaInternas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaInterna auditoriaInterna = db.AuditoriaInternas.Find(id);
            if (auditoriaInterna == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                           CombosHelper.GetYears(),
                           "YearId",
                           "Name");
            return View(auditoriaInterna);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuditoriaInterna auditoriaInterna)
        {
            if (ModelState.IsValid)
            {
                if (auditoriaInterna.AdjuntoFile != null)
                {
                    var folder = "~/Content/AuditoriasInternas";
                    var file = string.Format("{0}_{1}", auditoriaInterna.AuditoriaInternaId, auditoriaInterna.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(auditoriaInterna.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        auditoriaInterna.Adjunto = pic;
                        db.Entry(auditoriaInterna).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                auditoriaInterna.DateEdition = Convert.ToDateTime(fecha);
                auditoriaInterna.AutorEdition = autor.FullName;
                db.Entry(auditoriaInterna).State = EntityState.Modified;
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
            return View(auditoriaInterna);
        }

        // GET: AuditoriaInternas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaInterna auditoriaInterna = db.AuditoriaInternas.Find(id);
            if (auditoriaInterna == null)
            {
                return HttpNotFound();
            }
            return View(auditoriaInterna);
        }

        // POST: AuditoriaInternas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuditoriaInterna auditoriaInterna = db.AuditoriaInternas.Find(id);
            db.AuditoriaInternas.Remove(auditoriaInterna);
            try
            {
                var response = FilesHelper.DeleteDocument(auditoriaInterna.Adjunto);
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
