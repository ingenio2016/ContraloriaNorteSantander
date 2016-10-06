using ContraloriaNDSWeb.Classes;
using ContraloriaNDSWeb.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContraloriaNDSWeb.Controllers
{
    public class FuncionAdvertenciasController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        public ActionResult FuncionesAdvertencia()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Funciones(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var funcion = db.FuncionAdvertencias.Where(x => x.CompanyId == 2 && x.Year.Name == id);
            if (funcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Year = id;
            return View(funcion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var funcion = db.FuncionAdvertencias.Where(x => x.CompanyId == user.CompanyId);
            return View(funcion.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionAdvertencia funcionAdvertencia = db.FuncionAdvertencias.Find(id);
            if (funcionAdvertencia == null)
            {
                return HttpNotFound();
            }
            return View(funcionAdvertencia);
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
            var funcion = new FuncionAdvertencia { CompanyId = user.CompanyId, };
            return View(funcion);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FuncionAdvertencia funcionAdvertencia)
        {
            if (ModelState.IsValid)
            {
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                funcionAdvertencia.Date = Convert.ToDateTime(fecha);
                funcionAdvertencia.Autor = autor.FullName;
                funcionAdvertencia.DateEdition = Convert.ToDateTime(fecha);
                funcionAdvertencia.AutorEdition = autor.FullName;
                db.FuncionAdvertencias.Add(funcionAdvertencia);
                try
                {
                    db.SaveChanges();
                    if (funcionAdvertencia.AdjuntoFile != null)
                    {
                        var folder = "~/Content/FuncionesAdvertencia";
                        var file = string.Format("{0}_{1}", funcionAdvertencia.FuncionAdvertenciaId, funcionAdvertencia.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(funcionAdvertencia.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            funcionAdvertencia.Adjunto = pic;
                            db.Entry(funcionAdvertencia).State = EntityState.Modified;
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
                }                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", funcionAdvertencia.CompanyId);
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", funcionAdvertencia.YearId);
            return View(funcionAdvertencia);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionAdvertencia funcionAdvertencia = db.FuncionAdvertencias.Find(id);
            if (funcionAdvertencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearId = new SelectList(
                          CombosHelper.GetYears(),
                          "YearId",
                          "Name");
            return View(funcionAdvertencia);
        }

        // POST: FuncionAdvertencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FuncionAdvertencia funcionAdvertencia)
        {
            if (ModelState.IsValid)
            {
                if (funcionAdvertencia.AdjuntoFile != null)
                {
                    var folder = "~/Content/PlanAccionAdjuntos";
                    var file = string.Format("{0}_{1}", funcionAdvertencia.FuncionAdvertenciaId, funcionAdvertencia.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(funcionAdvertencia.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        funcionAdvertencia.Adjunto = pic;
                        db.Entry(funcionAdvertencia).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                var fecha = DateTime.Now;
                var autor = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                
                funcionAdvertencia.DateEdition = Convert.ToDateTime(fecha);
                funcionAdvertencia.AutorEdition = autor.FullName;
                db.Entry(funcionAdvertencia).State = EntityState.Modified;
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
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", funcionAdvertencia.CompanyId);
            ViewBag.YearId = new SelectList(db.Years, "YearId", "Name", funcionAdvertencia.YearId);
            return View(funcionAdvertencia);
        }

        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FuncionAdvertencia funcionAdvertencia = db.FuncionAdvertencias.Find(id);
            if (funcionAdvertencia == null)
            {
                return HttpNotFound();
            }
            return View(funcionAdvertencia);
        }

        // POST: FuncionAdvertencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FuncionAdvertencia funcionAdvertencia = db.FuncionAdvertencias.Find(id);
            db.FuncionAdvertencias.Remove(funcionAdvertencia);
            try
            {
                var response = FilesHelper.DeleteDocument(funcionAdvertencia.Adjunto);
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
