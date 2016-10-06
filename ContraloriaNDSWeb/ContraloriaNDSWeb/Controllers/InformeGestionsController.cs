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
    public class InformeGestionsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult InformeGestion()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var informe = db.InformeGestions.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(informe.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var informe = db.InformeGestions.Where(x => x.CompanyId == user.CompanyId);
            return View(informe.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeGestion informeGestion = db.InformeGestions.Find(id);
            if (informeGestion == null)
            {
                return HttpNotFound();
            }
            return View(informeGestion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var informe = new InformeGestion { CompanyId = user.CompanyId, };
            return View(informe);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InformeGestion informeGestion)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                informeGestion.Date = Convert.ToDateTime(fecha);
                informeGestion.Autor = autor.FullName;
                informeGestion.DateEdition = Convert.ToDateTime(fecha);
                informeGestion.AutorEdition = autor.FullName;
                db.InformeGestions.Add(informeGestion);
                try
                {
                    db.SaveChanges();
                    if (informeGestion.AdjuntoFile != null)
                    {
                        var folder = "~/Content/InformesGestion";
                        var file = string.Format("{0}_{1}", informeGestion.InformeGestionId, informeGestion.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(informeGestion.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            informeGestion.Adjunto = pic;
                            db.Entry(informeGestion).State = EntityState.Modified;
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", informeGestion.CompanyId);
            return View(informeGestion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeGestion informeGestion = db.InformeGestions.Find(id);
            if (informeGestion == null)
            {
                return HttpNotFound();
            }
            return View(informeGestion);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InformeGestion informeGestion)
        {
            if (ModelState.IsValid)
            {
                if (informeGestion.AdjuntoFile != null)
                {
                    var folder = "~/Content/InformesGestion";
                    var file = string.Format("{0}_{1}", informeGestion.InformeGestionId, informeGestion.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(informeGestion.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        informeGestion.Adjunto = pic;
                        db.Entry(informeGestion).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
               
                informeGestion.DateEdition = Convert.ToDateTime(fecha);
                informeGestion.AutorEdition = autor.FullName;
                db.Entry(informeGestion).State = EntityState.Modified;
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
                }                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", informeGestion.CompanyId);
            return View(informeGestion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InformeGestion informeGestion = db.InformeGestions.Find(id);
            if (informeGestion == null)
            {
                return HttpNotFound();
            }
            return View(informeGestion);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InformeGestion informeGestion = db.InformeGestions.Find(id);
            db.InformeGestions.Remove(informeGestion);
            try
            {
                var response = FilesHelper.DeleteDocument(informeGestion.Adjunto);
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
