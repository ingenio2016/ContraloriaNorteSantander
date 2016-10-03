using ContraloriaNDSWeb.Classes;
using ContraloriaNDSWeb.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContraloriaNDSWeb.Controllers
{
    public class ControlInternoesController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult ControlInterno()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var control = db.ControlInternoes.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(control.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var control = db.ControlInternoes.Where(x => x.CompanyId == user.CompanyId);
            return View(control.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlInterno controlInterno = db.ControlInternoes.Find(id);
            if (controlInterno == null)
            {
                return HttpNotFound();
            }
            return View(controlInterno);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var control = new ControlInterno { CompanyId = user.CompanyId, };
            return View(control);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControlInterno controlInterno)
        {
            if (ModelState.IsValid)
            {
                db.ControlInternoes.Add(controlInterno);
                try
                {
                    db.SaveChanges();
                    if (controlInterno.AdjuntoFile != null)
                    {
                        var folder = "~/Content/ControlInterno";
                        var file = string.Format("{0}_{1}", controlInterno.ControlInternoId, controlInterno.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(controlInterno.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            controlInterno.Adjunto = pic;
                            db.Entry(controlInterno).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                catch (System.Exception ex)
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", controlInterno.CompanyId);
            return View(controlInterno);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlInterno controlInterno = db.ControlInternoes.Find(id);
            if (controlInterno == null)
            {
                return HttpNotFound();
            }
            return View(controlInterno);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ControlInterno controlInterno)
        {
            if (ModelState.IsValid)
            {
                if (controlInterno.AdjuntoFile != null)
                {
                    var folder = "~/Content/ControlInterno";
                    var file = string.Format("{0}_{1}", controlInterno.ControlInternoId, controlInterno.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(controlInterno.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        controlInterno.Adjunto = pic;
                        db.Entry(controlInterno).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(controlInterno).State = EntityState.Modified;
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
            return View(controlInterno);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControlInterno controlInterno = db.ControlInternoes.Find(id);
            if (controlInterno == null)
            {
                return HttpNotFound();
            }
            return View(controlInterno);
        }

        // POST: ControlInternoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControlInterno controlInterno = db.ControlInternoes.Find(id);
            db.ControlInternoes.Remove(controlInterno);
            try
            {
                var response = FilesHelper.DeleteDocument(controlInterno.Adjunto);
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
