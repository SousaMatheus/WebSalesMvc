using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public SellersController(SellerService sellerService, DepartmentService departmentService)//adcionado no construtor para que ele possa ser injetado
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()//chamou controlador
        {
            var list = await _sellerService.FindAllAsync();//controlador acessou model 
            return View(list);//encaminha os dados para view
        }
        public async Task<IActionResult> Create()//acao do tipo get http
        {
            var departments = await _departmentService.FindAllAsync();//instanciar os departamentos e traz todos do banco de dados do usando o servico
            var viewModel = new SellerFormViewModel { Departments = departments };//
            return View(viewModel);//chama a view Create, o nome da view deve ser exatamente Create
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)//acao do tipo POST 
        {
            if(!ModelState.IsValid)//verifica se a requisicao e valida sem necessidade do javascript
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments } ;
                return View(viewModel);//retornar a viewModel
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));//melhora a manutencao do codigo
        }
        public async Task<IActionResult> Delete(int? id)//metodo GET Para abrir uma tela de confirmacao
        {
            if(id == null)//verifica se o id é nullo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });//redirecionando para o controller Error e passando um objeto anonimo como argumento
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);// ao passar esse objeto para view o framework vai buscar uma pagina chamada Delete
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e )
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)//verifica se o id é nullo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if(obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)//verifica se o id é nullo
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if(obj == null)//verifica se o metodo nao encontrou nada de acordo com o id
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };//carregando a viewmodel com o seller que buscamos pelo id
                                                                                                                //e carregando a lista de todos os departments
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)//verifica se o Id da URL e diferente do Id da requisicao.
                                                        //Importante o objeto seller ter esse nome, caso seja outro nao atualiza!
        {
            if(!ModelState.IsValid)//verifica se a requisicao e valida sem precisar do javascript
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);//retornar a viewModel
            }

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });//nao correspondem
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException ex )//poderia ser utilizado superclasse ApplicationException e retornar a mensagem dela
            {

                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch(DbConccurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //macete do framework para buscar o Id da requisicao
            };
            return View(viewModel);
        }
    }
}
