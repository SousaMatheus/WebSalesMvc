using Microsoft.EntityFrameworkCore;//necessario para utilizar o  ToListAsync
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<Department>> FindAllAsync()
        {

            return await _context.Department.OrderBy(x => x.Name).ToListAsync();//ordena os departamentos por listas usando o Linq OrderBy
        }
    }
}
