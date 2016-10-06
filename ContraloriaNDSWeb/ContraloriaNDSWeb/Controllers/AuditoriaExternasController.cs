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
    public class AuditoriaExternasController : Controller
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
            var auditoria = db.AuditoriaExternas.Where(x => x.CompanyId == user.CompanyId);
            return View(auditoria.ToList());
        }

        // GET: AuditoriaInternas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaExterna auditoriaexterna = db.AuditoriaExternas.Find(id);
            if (auditoriaexterna == null)
            {
                return HttpNotFound();
            }
            return View(auditoriaexterna);
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
            var auditoria = new AuditoriaExterna { CompanyId = user.CompanyId, };
            return View(auditoria);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditoriaExterna auditoriaexterna)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                auditoriaexterna.Date = Convert.ToDateTime(fecha);
                auditoriaexterna.Autor = autor.FullName;
                auditoriaexterna.DateEdition = Convert.ToDateTime(fecha);
                auditoriaexterna.AutorEdition = autor.FullName;
                db.AuditoriaExternas.Add(auditoriaexterna);
                try
                {
                    db.SaveChanges();
                    if (auditoriaexterna.AdjuntoFile != null)
                    {
                        var folder = "~/Content/AuditoriasExternas";
                        var file = string.Format("{0}_{1}", auditoriaexterna.AuditoriaExternaId, auditoriaexterna.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(auditoriaexterna.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            auditoriaexterna.Adjunto = pic;
                            db.Entry(auditoriaexterna).State = EntityState.Modified;
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
            return View(auditoriaexterna);
        }

        // GET: AuditoriaInternas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaExterna auditoriaexterna = db.AuditoriaExternas.Find(id);
            if (auditoriaexterna == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                           CombosHelper.GetYears(),
                           "YearId",
                           "Name");
            return View(auditoriaexterna);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuditoriaExterna auditoriaexterna)
        {
            if (ModelState.IsValid)
            {
                if (auditoriaexterna.AdjuntoFile != null)
                {
                    var folder = "~/Content/AuditoriasExternas";
                    var file = string.Format("{0}_{1}", auditoriaexterna.AuditoriaExternaId, auditoriaexterna.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(auditoriaexterna.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        auditoriaexterna.Adjunto = pic;
                        db.Entry(auditoriaexterna).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                auditoriaexterna.DateEdition = Convert.ToDateTime(fecha);
                auditoriaexterna.AutorEdition = autor.FullName;
                db.Entry(auditoriaexterna).State = EntityState.Modified;
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
            return View(auditoriaexterna);
        }

        // GET: AuditoriaInternas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditoriaExterna auditoriaexterna = db.AuditoriaExternas.Find(id);
            if (auditoriaexterna == null)
            {
                return HttpNotFound();
            }
            return View(auditoriaexterna);
        }

        // POST: AuditoriaInternas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuditoriaExterna auditoriaexterna = db.AuditoriaExternas.Find(id);
            db.AuditoriaExternas.Remove(auditoriaexterna);
            try
            {
                var response = FilesHelper.DeleteDocument(auditoriaexterna.Adjunto);
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
