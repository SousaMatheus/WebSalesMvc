using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
using WebSalesMvc.Models.ViewModels;

using WebSalesMvc.Services;

namespace WebSalesMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;// dependencia com DepartmentService

        public SellersController (SellerService sellerService, DepartmentService departmentService)//adcionado no construtor para que ele possa ser injetado
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()//chamou controlador
        {
            var list = _sellerService.FindAll();//controlador acessou model 
            return View(list);//encaminha os dados para view
        }
        public IActionResult Create()//acao do tipo get http
        {
            var departments = _departmentService.FindAll();//instanciar os departamentos e traz todos do banco de dados do usando o servico
            var viewModel = new SellerFormViewModel { Departments = departments };//
            return View(viewModel);//chama a view Create, o nome da view deve ser exatamente Create
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
