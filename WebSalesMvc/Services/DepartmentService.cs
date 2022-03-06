using System;
using System.Collections.Generic;
using System.Linq;
using WebSalesMvc.Data;
using WebSalesMvc.Models;

namespace WebSalesMvc.Services
{
    public class DepartmentService
    {
        //classe precisa ter uma dependencia com db context
        private readonly WebSalesMvcContext _context;
        public DepartmentService(WebSalesMvcContext context)
        {
            _context = context;
        }
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();//ordena os departamentos por listas usando o Linq OrderBy
        }
    }
}
