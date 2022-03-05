using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Data;
using WebSalesMvc.Models;

namespace WebSalesMvc.Services
{
    public class SellerService
    {
        //classe precisa ter uma dependencia com db context
        private readonly WebSalesMvcContext _context;
        public SellerService (WebSalesMvcContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//retorna do banco de dados todos os vendedores
        }
        public void Insert(Seller obj)
        {
            obj.Department = _context.Department.First();//medida para adicionar um departamento provisoriamente
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
