using LaTienda.Clientes.AFIP;
using LaTienda.Models;
using LaTienda.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LaTienda.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IClienteAfip _clienteAfip;
        private ILoginTicketRepository _loginTicketRepository;
        public HomeController(ILogger<HomeController> logger, IClienteAfip clienteAfip, ILoginTicketRepository loginTicketRepository)
        {
            _logger = logger;
            _clienteAfip = clienteAfip;
            _loginTicketRepository = loginTicketRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
