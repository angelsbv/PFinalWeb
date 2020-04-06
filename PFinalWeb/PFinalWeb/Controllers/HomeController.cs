using PFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace PFinalWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly PFinalWebContext ctx;

        public HomeController()
        {
            PFinalWebContext context = new PFinalWebContext();
            ctx = context;
        }

        public ActionResult Index()
        {
            return View(ctx.Noticias.ToList());
        }

        /*Noticias*/
        public ActionResult News()
        {
            return View(ctx.Noticias.ToList());
        }
        
        [HttpGet]
        public ActionResult AgregarNoticia()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarNoticia(Noticias noti)
        {
            if(Request.Files.Count > 0)
            {
                noti.file = Request.Files[0];
            }
            else
            {
                ModelState.AddModelError("postedImg", "Debe seleccionar una imagen.");
                return View(noti);
            }

            HttpPostedFileBase postedFile = noti.file;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(fileName);

            if(fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bpm" ||
                fileExtension.ToLower() == ".png"||
                fileExtension.ToLower() == ".jpeg" ||
                fileExtension.ToLower() == ".tiff")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader br = new BinaryReader(stream);
                byte[] bytes = br.ReadBytes((int)stream.Length);
                noti.Foto = bytes;
                noti.Fecha = DateTime.Now;
                ctx.Configuration.ValidateOnSaveEnabled = false;
                ctx.Noticias.Add(noti);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("postedImg", "Solo se permiten imagenes.");
                return View(noti);
            }
        }

        [HttpGet]
        //[Authorize]
        public ActionResult EditNoticia(int? id)
        {
            if (id != null)
            {
                var noti = ctx.Noticias.Where(a => a.IDNew == id).FirstOrDefault();
                if (noti != null)
                {
                    return View(noti);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult EditNoticia(Noticias noti)
        {
            var c = ctx.Noticias.Find(noti.IDNew);
            if (c != null)
            {
                if (Request.Files.Count >= 1)
                {
                    noti.file = Request.Files[0];

                    HttpPostedFileBase postedFile = noti.file;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bpm" ||
                        fileExtension.ToLower() == ".png" ||
                        fileExtension.ToLower() == ".jpeg" ||
                        fileExtension.ToLower() == ".tiff")
                    {
                        Stream stream = postedFile.InputStream;
                        BinaryReader br = new BinaryReader(stream);
                        byte[] bytes = br.ReadBytes((int)stream.Length);
                        c.Foto = bytes;
                        noti.Fecha = DateTime.Now;
                        c.Titulo = noti.Titulo;
                        c.Resumen = noti.Resumen;
                        c.Contenido = noti.Contenido;
                        ctx.Configuration.ValidateOnSaveEnabled = false;
                        ctx.SaveChanges();
                        return RedirectToAction(nameof(News));
                    }
                    else if(fileName == "")
                    {
                        noti.Fecha = DateTime.Now;
                        c.Titulo = noti.Titulo;
                        c.Resumen = noti.Resumen;
                        c.Contenido = noti.Contenido;
                        ctx.Configuration.ValidateOnSaveEnabled = false;

                        ctx.SaveChanges();
                        return RedirectToAction(nameof(News));
                    }
                    else
                    {
                        ModelState.AddModelError("postedImg", "Solo se permiten imagenes.");
                        return View(noti);
                    }
                }
                else {
                    noti.Fecha = DateTime.Now;
                    c.Titulo = noti.Titulo;
                    c.Resumen = noti.Resumen;
                    c.Contenido = noti.Contenido;
                    ctx.Configuration.ValidateOnSaveEnabled = false;

                    ctx.SaveChanges();
                    return RedirectToAction(nameof(News));
                }
            }
            else
            {
                ModelState.AddModelError("errr", "Hubo error. Al parecer ha digitado datos incorrectos.");
                return View(noti);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DelNoticia(int id)
        {
            if (ModelState.IsValid)
            {
                var n = ctx.Noticias.Where(a => a.IDNew == id).FirstOrDefault();
                ctx.Noticias.Remove(n);
                ctx.SaveChanges();
            }
            return RedirectToAction(nameof(News));
        }
        /*./Noticias*/

        /*Casos*/
        public ActionResult Casos(int? id)
        {
            if (id != null)
            {
                ViewBag.Selected = id;
            }
            return View(ctx.Casos.ToList());
        }

        [HttpGet]
        [Authorize]
        public ActionResult AgregarCaso()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AgregarCaso(Casos p)
        {
            if (ModelState.IsValid)
            {
                ctx.Casos.Add(p);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditCaso(int? id)
        {
            if (id != null)
            {
                var caso = ctx.Casos.Where(a => a.IDCaso == id).FirstOrDefault();
                if (caso != null)
                {
                    return View(caso);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditCaso(Casos caso)
        {
            if (ModelState.IsValid)
            {
                var c = ctx.Casos.Find(caso.IDCaso);
                c.Nombre = caso.Nombre;
                c.Apellido = caso.Apellido;
                c.Cedula = caso.Cedula;
                c.Ciudad = caso.Ciudad;
                c.País = caso.País;
                c.Latitud = caso.Latitud;
                c.Longitud = caso.Longitud;
                c.FechaNacimiento = caso.FechaNacimiento;
                c.FechaContagio = caso.FechaContagio;
                c.Comentario = caso.Comentario;
                ctx.SaveChanges();
                return RedirectToAction(nameof(Casos));
                //PropertyInfo[] properties = typeof(Casos).GetProperties();
                //foreach (PropertyInfo p in properties)
                //{
                //    p.Name
                //    Console.WriteLine("Propiedad: " + p.Name);
                //    Console.WriteLine("Valor: " + property.GetValue(obj));
                //}
            }
            return View(caso);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DelCaso(int id)
        {
            if (ModelState.IsValid)
            {
                var caso = ctx.Casos.Where(a => a.IDCaso == id).FirstOrDefault();
                ctx.Casos.Remove(caso);
                ctx.SaveChanges();
            }
            return RedirectToAction(nameof(Casos));
        }

        /*./Casos*/

        public ActionResult Mapa()
        {
            return View();
        }

        public ActionResult Stats()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }
    }
}