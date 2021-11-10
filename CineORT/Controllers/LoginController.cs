﻿using CineORT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CineORT.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly CineDbContext _context;
        private const string _Return_Url = "ReturnUrl";

        public LoginController(CineDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            // Guardamos la url de retorno para que una vez concluído el login del 
            // usuario lo podamos redirigir a la página en la que se encontraba antes
            TempData[_Return_Url] = returnUrl;
            return View();

        }


        public static Usuario ObtenerUsuario(CineDbContext _context, string email, string password)
        {

            Usuario usuario = _context.Cliente.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Contrasenia);
            if (usuario == null)
                usuario = _context.Administradors.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Contrasenia);
            return usuario;
        }

        [HttpPost]
        public IActionResult Login(string email, string password, TipoUsuario tipo)
        {
            string returnUrl = TempData[_Return_Url] as string;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {

                var usuario = ObtenerUsuario(_context, email, password);

                if (usuario != null)
                {
                    // Se crean las credenciales del usuario que serán incorporadas al contexto
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    // El lo que luego obtendré al acceder a User.Identity.Name
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.NombreApellido));

                    // Se utilizará para la autorización por roles
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString()));

                    // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    // Lo utilizaremos cuando querramos mostrar el nombre del usuario logueado en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.NombreApellido));

                    //identity.AddClaim(new Claim(nameof(Usuario.Foto), usuario.Foto ?? string.Empty));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // En este paso se hace el login del usuario al sistema
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    TempData["LoggedIn"] = true;

                    //if (!string.IsNullOrWhiteSpace(returnUrl))
                    //    return Redirect(returnUrl);

                    if (tipo == TipoUsuario.Cliente)
                    {
                        return RedirectToAction(nameof(ReservasController.Create), "Reservar");
                    }
                    else
                    {
                        return RedirectToAction(nameof(AdministradorsController.MenuPrincipalAdministrador), "Menú Principal");
                    }


                }
            }

            // Completo estos dos campos para poder retornar a la vista en caso de errores.
            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.Email = email;
            TempData[_Return_Url] = returnUrl;

            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

      //  [Authorize]
     //   [HttpGet]
      //  public IActionResult NoAutorizado()
      //  {
     //       return View();
      //  }


    }
}

