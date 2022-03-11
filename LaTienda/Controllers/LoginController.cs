using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IEmpleadoRepository _empleadoRepository;

        public LoginController(IConfiguration config, IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Empleado login)
        {
            var user = AuthenticateUser(login);
            if (user == null)
            {
                return BadRequest();
            }

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{user.Legajo}:{user.CodigoSucursal}"),
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await Request.HttpContext.SignOutAsync();
            return View("Login");
        }


        private Empleado AuthenticateUser(Empleado login)
        {
            Empleado user = _empleadoRepository.GetByLegajo(login.Legajo); 
            if (user != null && login.Password == user.Password)
            {
                return user;
            }
            return null;
        }
    }
}