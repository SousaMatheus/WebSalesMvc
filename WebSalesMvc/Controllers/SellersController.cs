using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
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
        public IActionResult Create()//acao do tipo get http
        {
            return View();//chama a view Create, o nome da view deve ser exatamente Create
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Seller seller)//acao do tipo POST 
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//melhora a manutencao do codigo
        }
    }
}
