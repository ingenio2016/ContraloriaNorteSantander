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
    public class PlanAnticorrupcionsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult PlanesAntiCorrupcion(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var planescorrupcion = db.PlanAnticorrupcions.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (planescorrupcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(planescorrupcion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var planAnti = db.PlanAnticorrupcions.Where(x => x.CompanyId == user.CompanyId);
            return View(planAnti.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanAnticorrupcion planAnticorrupcion = db.PlanAnticorrupcions.Find(id);
            if (planAnticorrupcion == null)
            {
                return HttpNotFound();
            }
            return View(planAnticorrupcion);
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
            var plancorrupcion = new PlanAnticorrupcion { CompanyId = user.CompanyId, };
            return View(plancorrupcion);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanAnticorrupcion planAnticorrupcion)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                planAnticorrupcion.Date = Convert.ToDateTime(fecha);
                planAnticorrupcion.Autor = autor.FullName;
                planAnticorrupcion.DateEdition = Convert.ToDateTime(fecha);
                planAnticorrupcion.AutorEdition = autor.FullName;
                db.PlanAnticorrupcions.Add(planAnticorrupcion);
                try
                {
                    db.SaveChanges();
                    if (planAnticorrupcion.AdjuntoFile != null)
                    {
                        var folder = "~/Content/PlanAntiCAdjuntos";
                        var file = string.Format("{0}_{1}", planAnticorrupcion.PlanAnticorrupcionId, planAnticorrupcion.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(planAnticorrupcion.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            planAnticorrupcion.Adjunto = pic;
                            db.Entry(planAnticorrupcion).State = EntityState.Modified;
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", planAnticorrupcion.CompanyId);
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", planAnticorrupcion.YearId);
            return View(planAnticorrupcion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanAnticorrupcion planAnticorrupcion = db.PlanAnticorrupcions.Find(id);
            if (planAnticorrupcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", planAnticorrupcion.YearId);
            return View(planAnticorrupcion);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanAnticorrupcion planAnticorrupcion)
        {
            if (ModelState.IsValid)
            {
                if (planAnticorrupcion.AdjuntoFile != null)
                {
                    var folder = "~/Content/PlanAntiCAdjuntos";
                    var file = string.Format("{0}_{1}", planAnticorrupcion.PlanAnticorrupcionId, planAnticorrupcion.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(planAnticorrupcion.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        planAnticorrupcion.Adjunto = pic;
                        db.Entry(planAnticorrupcion).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
               
                planAnticorrupcion.DateEdition = Convert.ToDateTime(fecha);
                planAnticorrupcion.AutorEdition = autor.FullName;
                db.Entry(planAnticorrupcion).State = EntityState.Modified;
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
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", planAnticorrupcion.YearId);
            return View(planAnticorrupcion);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanAnticorrupcion planAnticorrupcion = db.PlanAnticorrupcions.Find(id);
            if (planAnticorrupcion == null)
            {
                return HttpNotFound();
            }
            return View(planAnticorrupcion);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanAnticorrupcion planAnticorrupcion = db.PlanAnticorrupcions.Find(id);
            db.PlanAnticorrupcions.Remove(planAnticorrupcion);
            try
            {
                var response = FilesHelper.DeleteDocument(planAnticorrupcion.Adjunto);
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
