using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
using WebSalesMvc.Models.ViewModels;

namespace WebSalesMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //tipo generico, cada tipo tem um metodo que gera um retorno diferente
        {
            return View();
        }
        //é chamado o controlador por padrao, e ele faz a
        public IActionResult About()//se digitar home/about trará essa pagina.
        {
            ViewData["Message"] = "Your application description page.";//obj ViewData, dictionary com chave e valor
            ViewData["Teste"] = "Colocando mais um valor nessa pagina.";

            return View();//retorna um IActionResult
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
