﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
using WebSalesMvc.Models.ViewModels;

using WebSalesMvc.Services;
using WebSalesMvc.Services.Exceptions;

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
        public IActionResult Delete(int? id)//metodo GET Para abrir uma tela de confirmacao
        {
            if (id == null)//verifica se o id é nullo
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);

            if (obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return NotFound();
            }
            return View(obj);// ao passar esse objeto para view o framework vai buscar uma pagina chamada Delete
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)//verifica se o id é nullo
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);

            if (obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult Edit (int? id)
        {
            if (id == null)//verifica se o id é nullo
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);

            if (obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return NotFound();
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };//carregando a viewmodel com o seller que buscamos pelo id
                                                                                                                //e carregando a lista de todos os departments
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Seller seller)//verifica se o Id da URL e diferente do Id da requisicao.
                                                         //Importante o objeto seller ter esse nome, caso seja outro nao atualiza!
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {

                return NotFound();//provisorio
            }
            catch (DbConccurrencyException)
            {
                return BadRequest();//provisorio
            }

        }
    }
}
