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
    public class JurisdiccionCoactivasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult JurisdiccionCoactiva()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var jurisdiccion = db.JurisdiccionCoactivas.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(jurisdiccion.ToList());
        }


        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var jurisdiccion = db.JurisdiccionCoactivas.Where(x => x.CompanyId == user.CompanyId);
            return View(jurisdiccion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JurisdiccionCoactiva jurisdiccionCoactiva = db.JurisdiccionCoactivas.Find(id);
            if (jurisdiccionCoactiva == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccionCoactiva);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var jurisdiccion = new JurisdiccionCoactiva { CompanyId = user.CompanyId, };
            return View(jurisdiccion);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JurisdiccionCoactiva jurisdiccionCoactiva)
        {
            if (ModelState.IsValid)
            {
                db.JurisdiccionCoactivas.Add(jurisdiccionCoactiva);
                try
                {
                    db.SaveChanges();
                    if (jurisdiccionCoactiva.AdjuntoFile != null)
                    {
                        var folder = "~/Content/Jurisdicciones";
                        var file = string.Format("{0}_{1}", jurisdiccionCoactiva.JurisdiccionCoactivaId, jurisdiccionCoactiva.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(jurisdiccionCoactiva.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            jurisdiccionCoactiva.Adjunto = pic;
                            db.Entry(jurisdiccionCoactiva).State = EntityState.Modified;
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", jurisdiccionCoactiva.CompanyId);
            return View(jurisdiccionCoactiva);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JurisdiccionCoactiva jurisdiccionCoactiva = db.JurisdiccionCoactivas.Find(id);
            if (jurisdiccionCoactiva == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccionCoactiva);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JurisdiccionCoactiva jurisdiccionCoactiva)
        {
            if (ModelState.IsValid)
            {
                if (jurisdiccionCoactiva.AdjuntoFile != null)
                {
                    var folder = "~/Content/Jurisdicciones";
                    var file = string.Format("{0}_{1}", jurisdiccionCoactiva.JurisdiccionCoactivaId, jurisdiccionCoactiva.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(jurisdiccionCoactiva.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        jurisdiccionCoactiva.Adjunto = pic;
                        db.Entry(jurisdiccionCoactiva).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(jurisdiccionCoactiva).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", jurisdiccionCoactiva.CompanyId);
            return View(jurisdiccionCoactiva);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JurisdiccionCoactiva jurisdiccionCoactiva = db.JurisdiccionCoactivas.Find(id);
            if (jurisdiccionCoactiva == null)
            {
                return HttpNotFound();
            }
            return View(jurisdiccionCoactiva);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JurisdiccionCoactiva jurisdiccionCoactiva = db.JurisdiccionCoactivas.Find(id);
            db.JurisdiccionCoactivas.Remove(jurisdiccionCoactiva);
            try
            {
                var response = FilesHelper.DeleteDocument(jurisdiccionCoactiva.Adjunto);
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
