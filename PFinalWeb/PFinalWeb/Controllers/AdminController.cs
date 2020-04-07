using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PFinalWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PFinalWeb.Controllers
{
    public class AdminController : Controller
    {

        private readonly PFinalWebContext ctx;

        public AdminController()
        {
            PFinalWebContext context = new PFinalWebContext();
            ctx = context;
        }

        //[Authorize]
        public ActionResult AdminPC()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration([Bind(Exclude = "IsEmailVerified, VerficationCode, RegisterDate")] Admins admin)
        {
            string msg = "";
            bool status = false;
            admin.RegisterDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (ctx.Admins.Any(e => e.Email == admin.Email))
                {
                    ModelState.AddModelError("EEKey", "Este correo electrónico no está disponible."); //Email Exists Key
                    return View(admin);
                }

                if(ctx.Admins.Any(e => e.UserName == admin.UserName))
                {
                    ModelState.AddModelError("UEKey", "Este nombre de usuario ya está en uso."); //User Exists Key
                    return View(admin);
                }

                admin.VerificationCode = Guid.NewGuid();

                admin.Password = Crypto.Hash(admin.Password);
                admin.ConfirmPassword = Crypto.Hash(admin.ConfirmPassword);

                admin.IsEmailVerified = false;
                ctx.Admins.Add(admin);
                await ctx.SaveChangesAsync();

                SendVerificationCode(admin.Email, admin.VerificationCode.ToString());
                msg = "Su registro se ha completado exitosamente." +
                    "Ahora debe activar su cuenta accediendo a su correo electrónico.";
                status = true;
            }
            ViewBag.Msg = msg;
            ViewBag.Status = status;
            return View(admin);
        }

        [HttpGet]
        public async Task<ActionResult> VerifyAccount(string id)
        {
            bool status = false;
            ctx.Configuration.ValidateOnSaveEnabled = false;
            var adm = ctx.Admins.Where(a => a.VerificationCode == new Guid(id)).FirstOrDefault();
            if(adm != null && !adm.IsEmailVerified) 
            {
                adm.IsEmailVerified = true;
                await ctx.SaveChangesAsync();
                status = true;
            }
            else
            {
                ViewBag.Msg = "Solicitud inválida. Al parecer tu correo electrónico ya está validado";
            }
            ViewBag.Status = status;
            return View();
        }

        [NonAction]
        public void SendVerificationCode(string email, string activationCode, string emailFor="VerifyAccount")
        {
            #pragma warning disable IDE0059
            var verifyUrl = "/Admin/"+emailFor+"/"+activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("prod4app@gmail.com", "Don't need answer");
            var toEmail = new MailAddress(email);
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("DHzLChr76Sw@K7kw5XkGFYQ=="));
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
            var fromEmailPW = Crypto.sbcdec(new byte[]{ 52, 112, 112, 112, 114, 48, 54, 54, });
            string subject="", body="";
            if(emailFor == "VerifyAccount")
            {
                subject = $"¡Tu cuenta ha sido creada!";
                body = "<br/><br/> " +
                    "Ahora necesitamos comprobar que eres tu. Haz click en el siguiente enlace para verificar tu cuenta " +
                    "y así completar tu registro: " +
                    "<a href=" + link + ">" + link + "</a>";
            }
            else if(emailFor == "ResetPW")
            {
                subject = $"Recuperación de cuenta";
                body = "<br/><br/> " +
                    "Hemos recibido una solicitud de recuperación de cuenta. " +
                    "Si desea recuperar su cuenta haga click en el siguiente enlace: " +
                    "<a href=" + link + ">" + link + "</a> de lo contrario borre este correo electrónico." +
                    "<br/><br/><small>Este mensaje puede contener información confidencial. Por lo que si usted no " +
                    "ha solicitado esta recuperación debe ignorar y/o eliminar el mismo.</small>";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPW),
            };

            using (var msg = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) smtp.Send(msg);
            #pragma warning restore IDE0059
        }

        [HttpGet]
        public ActionResult Login(string username)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AdminLogin admlogin, string returnUrl="")
        {
            string msg = "";

            var al = await (from a in ctx.Admins
                      where admlogin.Username == a.UserName
                      select a).FirstOrDefaultAsync();
            if(al != null && (string.Compare(Crypto.Hash(admlogin.Password), al.Password) == 0))
            {
                var verified = await (from a in ctx.Admins
                                        where al.IsEmailVerified
                                        select a).FirstOrDefaultAsync();
                if (verified != null)
                {
                    int timeout = 525600;
                    var ticket = new FormsAuthenticationTicket(admlogin.Username, admlogin.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
                    {
                        Expires = DateTime.Now.AddMinutes(timeout),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    msg = "Actualmente, esta cuenta se encuentra en proceso de registro. " +
                        "Vuelva a intentarlo una vez el proceso se haya completado.";
                }
            }
            else
            {
                msg = "Algo no anda bien con sus credenciales. Estamos seguros que digitó datos incorrectos.";
            }
            TempData["Msg"] = msg;
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Recover()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recover(string UserEmail)
        {
            string res = "";
            bool status = false;
            var admUE = (from a in ctx.Admins
                      where a.UserName == UserEmail || a.Email == UserEmail
                      select a).FirstOrDefault();
            if(admUE != null)
            {
                string recoverCode = Guid.NewGuid().ToString();
                SendVerificationCode(admUE.Email, recoverCode, "ResetPW");
                admUE.RecoverPWDCode = recoverCode;
                ctx.Configuration.ValidateOnSaveEnabled = false;
                ctx.SaveChanges();
                status = true;
                res = "Ahora necesitamos verificar que eres tu. " +
                    "Hemos enviado el código de verificación a tu correo electrónico.";
            }
            else
            {
                res = "La cuenta no existe o no se encuentra disponible.";
            }
            ViewBag.Status = status;
            ViewBag.Msg = res;
            return View();
        }

        public ActionResult ResetPW(string id)
        {
            var adm = ctx.Admins.Where(a => a.RecoverPWDCode == id).FirstOrDefault();
            if (adm != null && id != null)
            {
                ResetPasswordModel model = new ResetPasswordModel
                    { ResetCode = id };
                return View(model);
            }
            else
            {
                return RedirectToAction("Error404", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPW(ResetPasswordModel rpm)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
                var adm = ctx.Admins.Where(a => a.RecoverPWDCode == rpm.ResetCode).FirstOrDefault();
                if(adm != null)
                {
                    adm.Password = Crypto.Hash(rpm.NewPassword);
                    adm.RecoverPWDCode = "";
                    ctx.Configuration.ValidateOnSaveEnabled = false;
                    ctx.SaveChanges();
                    msg = "La nueva contraseña ha sido establecida correctamente.";
                }
                else
                {
                    msg = "El código no es válido.";
                }
            }
            else
            {
                msg = "Ha ocurrido un error con su solicitud. Vuelva a intentarlo";
            }


            TempData["Msg"] = msg;
            return View(rpm);
        }
    }
}