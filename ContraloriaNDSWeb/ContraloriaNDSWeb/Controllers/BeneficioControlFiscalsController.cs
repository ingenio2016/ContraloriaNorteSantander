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

        // GET: BeneficioControlFiscals
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

        // GET: BeneficioControlFiscals/Details/5
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

        // GET: BeneficioControlFiscals/Create
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

        // GET: BeneficioControlFiscals/Edit/5
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

        // GET: BeneficioControlFiscals/Delete/5
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

        // POST: BeneficioControlFiscals/Delete/5
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
