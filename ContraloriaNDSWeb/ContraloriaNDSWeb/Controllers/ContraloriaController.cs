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
    
    public class ContraloriaController : Controller
    {
        private ContraloriandsContext db = new ContraloriandsContext();

        [Authorize(Roles = "User")]
        public ActionResult Admin_Controlamosa()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Institucional()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Admin_Misional()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Admin_ControlFiscal()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Nosotros()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Nosotros()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(contraloria);
        }

        public ActionResult Controlamos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        public ActionResult Planes()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        //OBJETIVOS
        public ActionResult Objetivos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var objetivos = db.Objetivoes.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(objetivos.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Objetivos()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var objetivos = db.Objetivoes.Where(x => x.CompanyId == user.CompanyId);
            return View(objetivos.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult Objetivo_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        public ActionResult Objetivo_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var objetivo = new Objetivo { CompanyId = user.CompanyId, };
            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Objetivo_Crear(Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivoes.Add(objetivo);
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
                return RedirectToAction("Admin_Objetivos");
            }

            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        public ActionResult Objetivo_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Objetivo_Editar(Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
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
                return RedirectToAction("Admin_Objetivos");
            }
            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        public ActionResult Objetivo_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Objetivo_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Objetivo_BorrarConfirmed(int id)
        {
            Objetivo objetivo = db.Objetivoes.Find(id);
            db.Objetivoes.Remove(objetivo);
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
            return RedirectToAction("Admin_Objetivos");
        }

        //PLANES Y PROGRAMAS
        public ActionResult PlanesProgramas()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(contraloria);
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_PlanesProgramas()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var planesprogramas = db.PlanesProgramas.Where(x => x.CompanyId == user.CompanyId);
            return View(planesprogramas.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult PlanesProgramas_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesProgramas planesprogramas = db.PlanesProgramas.Find(id);
            if (planesprogramas == null)
            {
                return HttpNotFound();
            }
            return View(planesprogramas);
        }

        [Authorize(Roles = "User")]
        public ActionResult PlanesProgramas_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var planesprogramas = new PlanesProgramas { CompanyId = user.CompanyId, };
            return View(planesprogramas);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlanesProgramas_Crear(PlanesProgramas planesprogramas)
        {
            if (ModelState.IsValid)
            {
                db.PlanesProgramas.Add(planesprogramas);
                try
                {
                    db.SaveChanges();
                    if (planesprogramas.AdjuntoFile != null)
                    {
                        var folder = "~/Content/Adjuntos";
                        var file = string.Format("{0}_{1}", planesprogramas.PlanesProgramasId, planesprogramas.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(planesprogramas.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            planesprogramas.Adjunto = pic;
                            db.Entry(planesprogramas).State = EntityState.Modified;
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
                return RedirectToAction("Admin_PlanesProgramas");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return View(planesprogramas);
        }

        [Authorize(Roles = "User")]
        public ActionResult PlanesProgramas_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesProgramas planesprogramas = db.PlanesProgramas.Find(id);
            if (planesprogramas == null)
            {
                return HttpNotFound();
            }
            return View(planesprogramas);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlanesProgramas_Editar(PlanesProgramas planesprogramas)
        {
            if (ModelState.IsValid)
            {
                if (planesprogramas.AdjuntoFile != null)
                {
                    var folder = "~/Content/Adjuntos";
                    var file = string.Format("{0}_{1}", planesprogramas.PlanesProgramasId, planesprogramas.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(planesprogramas.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        planesprogramas.Adjunto = pic;
                        db.Entry(planesprogramas).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(planesprogramas).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Admin_PlanesProgramas");
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
                
            }
            return View(planesprogramas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "User")]
        public ActionResult PlanesProgramas_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesProgramas planesprogramas = db.PlanesProgramas.Find(id);
            if (planesprogramas == null)
            {
                return HttpNotFound();
            }
            return View(planesprogramas);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("PlanesProgramas_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult PlanesProgramas_BorrarConfirmed(int id)
        {
            PlanesProgramas plamesprogramas = db.PlanesProgramas.Find(id);
            db.PlanesProgramas.Remove(plamesprogramas);
            try
            {
                var response = FilesHelper.DeleteDocument(plamesprogramas.Adjunto);
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
            return RedirectToAction("Admin_PlanesProgramas");
        }

        //PRINCIPIOS ETICOS

        public ActionResult PrincipiosEticos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var principios = db.PrincipiosEticos.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(principios.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_PrincipiosEticos()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var principioseticos = db.PrincipiosEticos.Where(x => x.CompanyId == user.CompanyId);
            return View(principioseticos.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult PrincipiosEticos_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrincipiosEticos principioseticos = db.PrincipiosEticos.Find(id);
            if (principioseticos == null)
            {
                return HttpNotFound();
            }
            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        public ActionResult PrincipiosEticos_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var principioseticos = new PrincipiosEticos { CompanyId = user.CompanyId, };
            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrincipiosEticos_Crear(PrincipiosEticos principioseticos)
        {
            if (ModelState.IsValid)
            {
                db.PrincipiosEticos.Add(principioseticos);
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
                return RedirectToAction("Admin_PrincipiosEticos");
            }

            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        public ActionResult PrincipiosEticos_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrincipiosEticos principioseticos = db.PrincipiosEticos.Find(id);
            if (principioseticos == null)
            {
                return HttpNotFound();
            }
            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrincipiosEticos_Editar(PrincipiosEticos principioseticos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(principioseticos).State = EntityState.Modified;
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
                return RedirectToAction("Admin_PrincipiosEticos");
            }
            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        public ActionResult PrincipiosEticos_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrincipiosEticos principioseticos = db.PrincipiosEticos.Find(id);
            if (principioseticos == null)
            {
                return HttpNotFound();
            }
            return View(principioseticos);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("PrincipiosEticos_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult PrincipiosEticos_BorrarConfirmed(int id)
        {
            PrincipiosEticos principioseticos = db.PrincipiosEticos.Find(id);
            db.PrincipiosEticos.Remove(principioseticos);
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
            return RedirectToAction("Admin_PrincipiosEticos");
        }

        //VALORES ETICOS

        public ActionResult ValoresEticos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var valores = db.ValoresEticos.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(valores.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_ValoresEticos()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var valores = db.ValoresEticos.Where(x => x.CompanyId == user.CompanyId);
            return View(valores.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult ValoresEticos_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValoresEticos valores = db.ValoresEticos.Find(id);
            if (valores == null)
            {
                return HttpNotFound();
            }
            return View(valores);
        }

        [Authorize(Roles = "User")]
        public ActionResult ValoresEticos_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var valores = new ValoresEticos { CompanyId = user.CompanyId, };
            return View(valores);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValoresEticos_Crear(ValoresEticos valores)
        {
            if (ModelState.IsValid)
            {
                db.ValoresEticos.Add(valores);
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
                return RedirectToAction("Admin_ValoresEticos");
            }

            return View(valores);
        }

        [Authorize(Roles = "User")]
        public ActionResult ValoresEticos_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValoresEticos valores = db.ValoresEticos.Find(id);
            if (valores == null)
            {
                return HttpNotFound();
            }
            return View(valores);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValoresEticos_Editar(ValoresEticos valores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valores).State = EntityState.Modified;
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
                return RedirectToAction("Admin_ValoresEticos");
            }
            return View(valores);
        }

        [Authorize(Roles = "User")]
        public ActionResult ValoresEticos_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValoresEticos valores = db.ValoresEticos.Find(id);
            if (valores == null)
            {
                return HttpNotFound();
            }
            return View(valores);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("ValoresEticos_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult ValoresEticos_BorrarConfirmed(int id)
        {
            ValoresEticos valores = db.ValoresEticos.Find(id);
            db.ValoresEticos.Remove(valores);
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
            return RedirectToAction("Admin_ValoresEticos");
        }

        //POLITICAS DE CALIDAD
        public ActionResult PoliticasCalidad()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var politicas = db.PoliticasCalidads.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(politicas.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_PoliticasCalidad()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var politicas = db.PoliticasCalidads.Where(x => x.CompanyId == user.CompanyId);
            return View(politicas.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult PoliticasCalidad_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticasCalidad politicas = db.PoliticasCalidads.Find(id);
            if (politicas == null)
            {
                return HttpNotFound();
            }
            return View(politicas);
        }

        [Authorize(Roles = "User")]
        public ActionResult PoliticasCalidad_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var politicas = new PoliticasCalidad { CompanyId = user.CompanyId, };
            return View(politicas);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PoliticasCalidad_Crear(PoliticasCalidad politica)
        {
            if (ModelState.IsValid)
            {
                db.PoliticasCalidads.Add(politica);
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
                return RedirectToAction("Admin_PoliticasCalidad");
            }

            return View(politica);
        }

        [Authorize(Roles = "User")]
        public ActionResult PoliticasCalidad_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticasCalidad politica = db.PoliticasCalidads.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PoliticasCalidad_Editar(PoliticasCalidad politica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politica).State = EntityState.Modified;
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
                return RedirectToAction("Admin_PoliticasCalidad");
            }
            return View(politica);
        }

        [Authorize(Roles = "User")]
        public ActionResult PoliticasCalidad_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticasCalidad politica = db.PoliticasCalidads.Find(id);
            if (politica == null)
            {
                return HttpNotFound();
            }
            return View(politica);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("PoliticasCalidad_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult PoliticasCalidad_BorrarConfirmed(int id)
        {
            PoliticasCalidad politica = db.PoliticasCalidads.Find(id);
            db.PoliticasCalidads.Remove(politica);
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
            return RedirectToAction("Admin_PoliticasCalidad");
        }

        //COMPROMISOS CONTRALORIA
        public ActionResult Compromisos()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var compromiso = db.Compromisoes.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(compromiso.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Compromisos()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var compromiso = db.Compromisoes.Where(x => x.CompanyId == user.CompanyId);
            return View(compromiso.ToList());
        }
        [Authorize(Roles = "User")]
        public ActionResult Compromisos_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromiso compromiso = db.Compromisoes.Find(id);
            if (compromiso == null)
            {
                return HttpNotFound();
            }
            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        public ActionResult Compromisos_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var compromiso = new Compromiso { CompanyId = user.CompanyId, };
            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compromisos_Crear(Compromiso compromiso)
        {
            if (ModelState.IsValid)
            {
                db.Compromisoes.Add(compromiso);
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
                return RedirectToAction("Admin_Compromisos");
            }

            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        public ActionResult Compromisos_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromiso compromiso = db.Compromisoes.Find(id);
            if (compromiso == null)
            {
                return HttpNotFound();
            }
            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compromisos_Editar(Compromiso compromiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compromiso).State = EntityState.Modified;
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
                return RedirectToAction("Admin_Compromisos");
            }
            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        public ActionResult Compromisos_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromiso compromiso = db.Compromisoes.Find(id);
            if (compromiso == null)
            {
                return HttpNotFound();
            }
            return View(compromiso);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Compromisos_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Compromisos_BorrarConfirmed(int id)
        {
            Compromiso compromiso = db.Compromisoes.Find(id);
            db.Compromisoes.Remove(compromiso);
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
            return RedirectToAction("Admin_Compromisos");
        }

        //DIRECTORIO FUNCIONARIOS

        public ActionResult Directorio()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var directorio = db.Directorios.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(directorio.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Directorio()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var directorio = db.Directorios.Where(x => x.CompanyId == user.CompanyId);
            return View(directorio.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Directorio_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorio directorio = db.Directorios.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }
            return View(directorio);
        }

        [Authorize(Roles = "User")]
        public ActionResult Directorio_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var directorio = new Directorio { CompanyId = user.CompanyId, };
            return View(directorio);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Directorio_Crear(Directorio directorio)
        {
            if (ModelState.IsValid)
            {
                db.Directorios.Add(directorio);
                try
                {
                    db.SaveChanges();
                    if (directorio.LogoFile != null)
                    {
                        var folder = "~/Content/Funcionarios";
                        var file = string.Format("{0}.jpg", directorio.CompanyId);
                        var response = FilesHelper.UploadPhoto(directorio.LogoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            directorio.Logo = pic;
                            db.Entry(directorio).State = EntityState.Modified;
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
                return RedirectToAction("Admin_Directorio");
            }

            return View(directorio);
        }

        [Authorize(Roles = "User")]
        public ActionResult Directorio_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorio directorio = db.Directorios.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }
            return View(directorio);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Directorio_Editar(Directorio directorio)
        {
            if (ModelState.IsValid)
            {
                if (directorio.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Funcionarios";
                    var file = string.Format("{0}.jpg", directorio.CompanyId);
                    var response = FilesHelper.UploadPhoto(directorio.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        directorio.Logo = pic;
                    }
                }
                db.Entry(directorio).State = EntityState.Modified;
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
                return RedirectToAction("Admin_Directorio");
            }
            return View(directorio);
        }

        [Authorize(Roles = "User")]
        public ActionResult Directorio_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directorio directorio = db.Directorios.Find(id);
            if (directorio == null)
            {
                return HttpNotFound();
            }
            return View(directorio);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Directorio_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Directorio_BorrarConfirmed(int id)
        {
            Directorio directorio = db.Directorios.Find(id);
            db.Directorios.Remove(directorio);
            try
            {
                var response = FilesHelper.DeleteDocument(directorio.Logo);
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
            return RedirectToAction("Admin_Directorio");
        }

        //ORGANIGRAMA
        public ActionResult Organigrama()
        {
            return View();
        }

        //PRESUPUESTOS

        public ActionResult Presupuesto()
        {
            var contraloria = db.Companies.Where(x => x.CompanyId == 2).FirstOrDefault();
            if (contraloria == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var presupuesto = db.Presupuestoes.Where(x => x.CompanyId == contraloria.CompanyId);
            return View(presupuesto.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Admin_Presupuesto()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var presupuesto = db.Presupuestoes.Where(x => x.CompanyId == user.CompanyId);
            return View(presupuesto.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult Presupuesto_Detalle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuesto presupuesto = db.Presupuestoes.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        [Authorize(Roles = "User")]
        public ActionResult Presupuesto_Crear()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var presupuesto = new Presupuesto { CompanyId = user.CompanyId, };
            return View(presupuesto);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Presupuesto_Crear(Presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                db.Presupuestoes.Add(presupuesto);
                try
                {
                    db.SaveChanges();
                    if (presupuesto.AdjuntoFile != null)
                    {
                        var folder = "~/Content/Presupuestos";
                        var file = string.Format("{0}_{1}", presupuesto.PresupuestoId, presupuesto.AdjuntoFile.FileName);
                        var response = FilesHelper.UploadPhoto(presupuesto.AdjuntoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            presupuesto.Adjunto = pic;
                            db.Entry(presupuesto).State = EntityState.Modified;
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
                return RedirectToAction("Admin_Presupuesto");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return View(presupuesto);
        }

        [Authorize(Roles = "User")]
        public ActionResult Presupuesto_Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuesto presupuesto = db.Presupuestoes.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Presupuesto_Editar(Presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                if (presupuesto.AdjuntoFile != null)
                {
                    var folder = "~/Content/Presupuestos";
                    var file = string.Format("{0}_{1}", presupuesto.PresupuestoId, presupuesto.AdjuntoFile.FileName);
                    var response = FilesHelper.UploadPhoto(presupuesto.AdjuntoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        presupuesto.Adjunto = pic;
                        db.Entry(presupuesto).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.Entry(presupuesto).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Admin_Presupuesto");
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

            }
            return View(presupuesto);
        }
       

        [Authorize(Roles = "User")]
        public ActionResult Presupuesto_Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuesto presupuesto = db.Presupuestoes.Find(id);
            if (presupuesto == null)
            {
                return HttpNotFound();
            }
            return View(presupuesto);
        }

        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Presupuesto_Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Presupuesto_BorrarConfirmed(int id)
        {
            Presupuesto presupuesto = db.Presupuestoes.Find(id);
            db.Presupuestoes.Remove(presupuesto);
            try
            {
                var response = FilesHelper.DeleteDocument(presupuesto.Adjunto);
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
            return RedirectToAction("Admin_Presupuesto");
        }
    }
}
