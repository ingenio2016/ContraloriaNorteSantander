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
    [Authorize(Roles = "User")]
    public class ConsejoesController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: Consejoes
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var consejo = db.Consejoes.Where(x => x.CompanyId == user.CompanyId);
            return View(consejo.ToList());
        }

        // GET: Consejoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejoes.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            return View(consejo);
        }

        // GET: Consejoes/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var consejo = new Consejo { CompanyId = user.CompanyId, };
            return View(consejo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Consejo consejo)
        {
            if (ModelState.IsValid)
            {
                db.Consejoes.Add(consejo);
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

            return View(consejo);
        }

        // GET: Consejoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejoes.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            return View(consejo);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Consejo consejo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consejo).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", consejo.CompanyId);
            return View(consejo);
        }

        // GET: Consejoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consejo consejo = db.Consejoes.Find(id);
            if (consejo == null)
            {
                return HttpNotFound();
            }
            return View(consejo);
        }

        // POST: Consejoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consejo consejo = db.Consejoes.Find(id);
            db.Consejoes.Remove(consejo);
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
