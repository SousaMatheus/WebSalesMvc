using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Services;

namespace WebSalesMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController (SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()//chamou controlador
        {
            var list = _sellerService.FindAll();//controlador acessou model 
            return View(list);//encaminha os dados para view
        }
    }
}
