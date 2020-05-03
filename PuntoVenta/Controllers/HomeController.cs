using PuntoVenta.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PuntoVenta.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [SessionExpireFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region LOGIN
        [Route("Ingreso")]
        public ActionResult LogIn()
        {
            log.InfoFormat("\n\n======================================== ENTRO A LOGIN ========================================");
            if (Session[Constantes.VAR_SESION] != null)
                return RedirectToAction("Index", "Pedidos");
            else
            {
                Session[Constantes.VAR_SESION] = null;
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.AddHeader("Cache-control", "no-store, must-revalidate, private, no-cache");
                Response.AddHeader("Pragma", "no-cache");
                Response.AddHeader("Expires", "0");
                Response.AppendToLog("window.location.reload();");
                return View();
            }
        }

        [Route("Ingreso")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "Usuario,Password")] Models.Seguridad.LogIn login)
        {
            log.InfoFormat("Intenta autenticacion Usuario: [{0}]", login.Usuario);
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = GlobalVariables.webClient.PostAsJsonAsync("Seguridad/Autentica", login).Result;
                    var usuarioSesion = response.Content.ReadAsAsync<Models.Api.UsuarioSesion>().Result;

                    if (usuarioSesion.ErrorCode == 0)
                    {
                        log.InfoFormat("Autenticacion correcta usuario: [{0}], rol: [{1}]", usuarioSesion.Usuario, usuarioSesion.Rol);
                        Session[Constantes.VAR_SESION] = usuarioSesion;
                        return RedirectToAction("Index", "Pedidos");
                    }
                    ModelState.AddModelError("Password", usuarioSesion.ErrorDetail);
                }
                catch (Exception ex)
                {
                    log.ErrorFormat("Ocurrio un error al consultar el usuario. Error: {0}", ex.Message);
                    ModelState.AddModelError("Password", ex.Message);
                }
            }
            return View(login);
        }
        #endregion

        [HttpGet]
        public ActionResult Logout()
        {

            Session[Constantes.VAR_SESION] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.AddHeader("Cache-control", "no-store, must-revalidate, private, no-cache");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");
            Response.AppendToLog("window.location.reload();");
            return Redirect("~/Ingreso");
        }
    }
}