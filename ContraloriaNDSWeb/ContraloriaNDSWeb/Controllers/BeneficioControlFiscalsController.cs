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
    public class BeneficioControlFiscalsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult BeneficiosControlFiscal()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var beneficio = db.BeneficioControlFiscals.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(beneficio.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var beneficio = db.BeneficioControlFiscals.Where(x => x.CompanyId == user.CompanyId);
            return View(beneficio.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficioControlFiscal beneficioControlFiscal = db.BeneficioControlFiscals.Find(id);
            if (beneficioControlFiscal == null)
            {
                return HttpNotFound();
            }
            return View(beneficioControlFiscal);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var beneficio = new BeneficioControlFiscal { CompanyId = user.CompanyId, };
            return View(beneficio);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BeneficioControlFiscal beneficioControlFiscal)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                beneficioControlFiscal.Date = Convert.ToDateTime(fecha);
                beneficioControlFiscal.Autor = autor.FullName;
                beneficioControlFiscal.DateEdition = Convert.ToDateTime(fecha);
                beneficioControlFiscal.AutorEdition = autor.FullName;
                db.BeneficioControlFiscals.Add(beneficioControlFiscal);
                try
                {
                    db.SaveChanges();
                    if (beneficioControlFiscal.AdjuntoFile != null)
                    {
                        var folder = "~/Content/BeneficiosControl";
                        var file = string.Format("{0}_{1}", beneficioControlFiscal.BeneficioControlFiscalId, beneficioControlFiscal.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(beneficioControlFiscal.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            beneficioControlFiscal.Adjunto = pic;
                            db.Entry(beneficioControlFiscal).State = EntityState.Modified;
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

            return View(beneficioControlFiscal);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficioControlFiscal beneficioControlFiscal = db.BeneficioControlFiscals.Find(id);
            if (beneficioControlFiscal == null)
            {
                return HttpNotFound();
            }
            return View(beneficioControlFiscal);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BeneficioControlFiscal beneficioControlFiscal)
        {
            if (ModelState.IsValid)
            {
                if (beneficioControlFiscal.AdjuntoFile != null)
                {
                    var folder = "~/Content/BeneficiosControl";
                    var file = string.Format("{0}_{1}", beneficioControlFiscal.BeneficioControlFiscalId, beneficioControlFiscal.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(beneficioControlFiscal.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        beneficioControlFiscal.Adjunto = pic;
                        db.Entry(beneficioControlFiscal).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                beneficioControlFiscal.DateEdition = Convert.ToDateTime(fecha);
                beneficioControlFiscal.AutorEdition = autor.FullName;
                db.Entry(beneficioControlFiscal).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", beneficioControlFiscal.CompanyId);
            return View(beneficioControlFiscal);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficioControlFiscal beneficioControlFiscal = db.BeneficioControlFiscals.Find(id);
            if (beneficioControlFiscal == null)
            {
                return HttpNotFound();
            }
            return View(beneficioControlFiscal);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BeneficioControlFiscal beneficioControlFiscal = db.BeneficioControlFiscals.Find(id);
            db.BeneficioControlFiscals.Remove(beneficioControlFiscal);
            try
            {
                var response = FilesHelper.DeleteDocument(beneficioControlFiscal.Adjunto);
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
