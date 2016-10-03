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
    public class RecursoHumanoesController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult RecursosHumanos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var recursos = db.RecursoHumanoes.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(recursos.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var recursos = db.RecursoHumanoes.Where(x => x.CompanyId == user.CompanyId);
            return View(recursos.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHumano recursoHumano = db.RecursoHumanoes.Find(id);
            if (recursoHumano == null)
            {
                return HttpNotFound();
            }
            return View(recursoHumano);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var recurso = new RecursoHumano { CompanyId = user.CompanyId, };
            return View(recurso);
        }

        // POST: RecursoHumanoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecursoHumano recursoHumano)
        {
            if (ModelState.IsValid)
            {
                db.RecursoHumanoes.Add(recursoHumano);
                try
                {
                    db.SaveChanges();
                    if (recursoHumano.AdjuntoFile != null)
                    {
                        var folder = "~/Content/AdjRecursosH";
                        var file = string.Format("{0}_{1}", recursoHumano.RecursoHumanoId, recursoHumano.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(recursoHumano.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            recursoHumano.Adjunto = pic;
                            db.Entry(recursoHumano).State = EntityState.Modified;
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", recursoHumano.CompanyId);
            return View(recursoHumano);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHumano recursoHumano = db.RecursoHumanoes.Find(id);
            if (recursoHumano == null)
            {
                return HttpNotFound();
            }
            return View(recursoHumano);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecursoHumano recursoHumano)
        {
            if (ModelState.IsValid)
            {
                if (recursoHumano.AdjuntoFile != null)
                {
                    var folder = "~/Content/AdjRecursosH";
                    var file = string.Format("{0}_{1}", recursoHumano.RecursoHumanoId, recursoHumano.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(recursoHumano.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        recursoHumano.Adjunto = pic;
                        db.Entry(recursoHumano).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(recursoHumano).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", recursoHumano.CompanyId);
            return View(recursoHumano);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoHumano recursoHumano = db.RecursoHumanoes.Find(id);
            if (recursoHumano == null)
            {
                return HttpNotFound();
            }
            return View(recursoHumano);
        }

        // POST: RecursoHumanoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoHumano recursoHumano = db.RecursoHumanoes.Find(id);
            db.RecursoHumanoes.Remove(recursoHumano);
            try
            {
                var response = FilesHelper.DeleteDocument(recursoHumano.Adjunto);
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
