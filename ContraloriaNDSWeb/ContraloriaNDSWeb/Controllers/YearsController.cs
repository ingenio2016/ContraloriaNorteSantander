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
    [Authorize(Roles = "Admin")]
    public class YearsController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: Years
        public ActionResult Index()
        {
            var years = db.Years.Include(y => y.Company);
            return View(years.ToList());
        }

        // GET: Years/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Year year = db.Years.Find(id);
            if (year == null)
            {
                return HttpNotFound();
            }
            return View(year);
        }

        // GET: Years/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(
                 CombosHelper.GetCompanies(),
                 "CompanyId",
                 "Name");
            return View();
        }

        // POST: Years/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Year year)
        {
            if (ModelState.IsValid)
            {
                db.Years.Add(year);
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

            return View(year);
        }

        // GET: Years/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Year year = db.Years.Find(id);
            if (year == null)
            {
                return HttpNotFound();
            }
            return View(year);
        }

        // POST: Years/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Year year)
        {
            if (ModelState.IsValid)
            {
                db.Entry(year).State = EntityState.Modified;
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
            return View(year);
        }

        // GET: Years/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Year year = db.Years.Find(id);
            if (year == null)
            {
                return HttpNotFound();
            }
            return View(year);
        }

        // POST: Years/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Year year = db.Years.Find(id);
            db.Years.Remove(year);
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
