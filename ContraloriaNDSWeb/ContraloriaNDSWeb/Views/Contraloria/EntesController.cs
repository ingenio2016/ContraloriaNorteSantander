using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContraloriaNDSWeb.Models;

namespace ContraloriaNDSWeb.Views.Contraloria
{
    [Authorize(Roles = "User")]
    public class EntesController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: Entes
        public ActionResult Index()
        {
            var entes = db.Entes.Include(e => e.Company);
            return View(entes.ToList());
        }

        // GET: Entes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entes entes = db.Entes.Find(id);
            if (entes == null)
            {
                return HttpNotFound();
            }
            return View(entes);
        }

        // GET: Entes/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var ente = new Entes { CompanyId = user.CompanyId, };
            return View(ente);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Entes entes)
        {
            if (ModelState.IsValid)
            {
                db.Entes.Add(entes);
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

            return View(entes);
        }

        // GET: Entes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entes entes = db.Entes.Find(id);
            if (entes == null)
            {
                return HttpNotFound();
            }
            return View(entes);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Entes entes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entes).State = EntityState.Modified;
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
            return View(entes);
        }

        // GET: Entes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entes entes = db.Entes.Find(id);
            if (entes == null)
            {
                return HttpNotFound();
            }
            return View(entes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entes entes = db.Entes.Find(id);
            db.Entes.Remove(entes);
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
