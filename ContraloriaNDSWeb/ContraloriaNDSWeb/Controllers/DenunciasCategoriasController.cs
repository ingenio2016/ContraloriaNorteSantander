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
    public class DenunciasCategoriasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        // GET: DenunciasCategorias
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var categorias = db.DenunciasCategorias.Where(x => x.CompanyId == user.CompanyId);
            return View(categorias.ToList());
        }

        // GET: DenunciasCategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenunciasCategorias denunciasCategorias = db.DenunciasCategorias.Find(id);
            if (denunciasCategorias == null)
            {
                return HttpNotFound();
            }
            return View(denunciasCategorias);
        }

        // GET: DenunciasCategorias/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var categoria = new DenunciasCategorias { CompanyId = user.CompanyId, };
            return View(categoria);
        }

        // POST: DenunciasCategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DenunciasCategorias denunciasCategorias)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                denunciasCategorias.Date = Convert.ToDateTime(fecha);
                denunciasCategorias.Autor = autor.FullName;
                denunciasCategorias.DateEdition = Convert.ToDateTime(fecha);
                denunciasCategorias.AutorEdition = autor.FullName;
                db.DenunciasCategorias.Add(denunciasCategorias);
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

            return View(denunciasCategorias);
        }

        // GET: DenunciasCategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenunciasCategorias denunciasCategorias = db.DenunciasCategorias.Find(id);
            if (denunciasCategorias == null)
            {
                return HttpNotFound();
            }
            return View(denunciasCategorias);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DenunciasCategorias denunciasCategorias)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                denunciasCategorias.DateEdition = Convert.ToDateTime(fecha);
                denunciasCategorias.AutorEdition = autor.FullName;
                db.Entry(denunciasCategorias).State = EntityState.Modified;
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
            return View(denunciasCategorias);
        }

        // GET: DenunciasCategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DenunciasCategorias denunciasCategorias = db.DenunciasCategorias.Find(id);
            if (denunciasCategorias == null)
            {
                return HttpNotFound();
            }
            return View(denunciasCategorias);
        }

        // POST: DenunciasCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DenunciasCategorias denunciasCategorias = db.DenunciasCategorias.Find(id);
            db.DenunciasCategorias.Remove(denunciasCategorias);
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
