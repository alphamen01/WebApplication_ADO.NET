using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_ADO.NET.DAL;
using WebApplication_ADO.NET.Models;
using WebApplication_ADO.NET.Permisos;

namespace WebApplication_ADO.NET.Controllers
{
    [NoAccesoCuandoSesionActiva]
    public class AccesoController : Controller
    {
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario usuario)
        {
            bool registrado;
            string mensaje;
            if (usuario.clave == usuario.confirmar_clave)
            {
                usuario.clave = usuarioDAL.ConvertirSha256(usuario.clave);

                (registrado, mensaje) = usuarioDAL.RegistrarUser(usuario);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            usuario.clave = usuarioDAL.ConvertirSha256(usuario.clave);
            int idUsuario = usuarioDAL.ValidarUser(usuario);

            if (idUsuario != 0)
            {
                Session["usuario"] = usuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }
    }
}