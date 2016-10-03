using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContraloriaNDSWeb.Models;

namespace ContraloriaNDSWeb.Controllers
{
    
    public class MisionVisionsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var misionvision = db.MisionVisions.Where(x => x.CompanyId == user.CompanyId);
            return View(misionvision.ToList());
        }

        public ActionResult Info()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var misionvision = db.MisionVisions.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(misionvision.FirstOrDefault());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MisionVision misionVision = db.MisionVisions.Find(id);
            if (misionVision == null)
            {
                return HttpNotFound();
            }
            return View(misionVision);
        }

        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var misionvision = new MisionVision { CompanyId = user.CompanyId, };
            return View(misionvision);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MisionVision misionVision)
        {
            if (ModelState.IsValid)
            {
                db.MisionVisions.Add(misionVision);
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", misionVision.CompanyId);
            return View(misionVision);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MisionVision misionVision = db.MisionVisions.Find(id);
            if (misionVision == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", misionVision.CompanyId);
            return View(misionVision);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MisionVision misionVision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(misionVision).State = EntityState.Modified;
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
            
            return View(misionVision);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MisionVision misionVision = db.MisionVisions.Find(id);
            if (misionVision == null)
            {
                return HttpNotFound();
            }
            return View(misionVision);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MisionVision misionVision = db.MisionVisions.Find(id);
            db.MisionVisions.Remove(misionVision);
            try
            {
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
