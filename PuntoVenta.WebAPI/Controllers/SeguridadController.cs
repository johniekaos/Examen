using PuntoVenta.DataAccess;
using PuntoVenta.WebAPI.Models;
using PuntoVenta.WebAPI.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PuntoVenta.WebAPI.Controllers
{
    public class SeguridadController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public IResponse Autentica(LoginRequest login)
        {
            var _response = new UsuarioSesion() { ErrorCode = 1, ErrorDetail = "Credenciales o Usuario inválido." };

            try
            {


                using (ExamenDatabase db = new ExamenDatabase())
                {
                    var usuarioDb = db.USUARIOS_W.Where(u => u.USERNAME == login.Usuario).FirstOrDefault();
                    if (usuarioDb != null)
                    {
                        if (usuarioDb.PASSWORD == login.Password)
                        {
                            _response = new UsuarioSesion()
                            {
                                Rol = usuarioDb.ROLE,
                                Usuario = usuarioDb.USERNAME,
                                Nombre = usuarioDb.NOMBRE,
                                Apellidos = usuarioDb.APELLIDOS

                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("error en autenticacion", ex);
                _response.ErrorCode = 2;
                _response.ErrorDetail = "Error en conexión";
            }
            return _response;
        }
    }
}
