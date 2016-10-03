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
    public class PersoneriasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: Personerias
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var personeria = db.Personerias.Where(x => x.CompanyId == user.CompanyId);
            return View(personeria.ToList());
        }

        // GET: Personerias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeria personeria = db.Personerias.Find(id);
            if (personeria == null)
            {
                return HttpNotFound();
            }
            return View(personeria);
        }

        // GET: Personerias/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var personeria = new Personeria { CompanyId = user.CompanyId, };
            return View(personeria);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Personeria personeria)
        {
            if (ModelState.IsValid)
            {
                db.Personerias.Add(personeria);
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

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", personeria.CompanyId);
            return View(personeria);
        }

        // GET: Personerias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeria personeria = db.Personerias.Find(id);
            if (personeria == null)
            {
                return HttpNotFound();
            }
            return View(personeria);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Personeria personeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personeria).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", personeria.CompanyId);
            return View(personeria);
        }

        // GET: Personerias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personeria personeria = db.Personerias.Find(id);
            if (personeria == null)
            {
                return HttpNotFound();
            }
            return View(personeria);
        }

        // POST: Personerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personeria personeria = db.Personerias.Find(id);
            db.Personerias.Remove(personeria);
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
