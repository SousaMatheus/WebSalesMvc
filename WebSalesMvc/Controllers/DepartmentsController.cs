using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;

namespace WebSalesMvc.Controllers
{
    public class DepartmentsController : Controller //para criar uma pasta na View Departments o nome precisa estar exatamente igual
    {
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department() { ID = 1, Name = "Eletronics" });
            list.Add(new Department() { ID = 2, Name = "Fashion" });
            return View(list);
        }
    }
}
