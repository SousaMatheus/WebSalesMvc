using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Data;
using WebSalesMvc.Models;
using Microsoft.EntityFrameworkCore;
using WebSalesMvc.Services.Exceptions;

namespace WebSalesMvc.Services
{
    public class SellerService
    {
        //classe precisa ter uma dependencia com db context
        private readonly WebSalesMvcContext _context;
        public SellerService(WebSalesMvcContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();//retorna do banco de dados todos os vendedores
        }
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);//necessario utilizar a expressao nao nativa do Linq
                                                                                                      //Include para realizar eager loading
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);//remove o obj do dbset
            _context.SaveChanges();//efetiva na bd
        }

        internal void Details(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(Seller obj)//atualizar um vendedor
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))//verifica se tem no banco de dados um vendedor com id igual do objeto
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new DbConccurrencyException(ex.Message);//intercepto a exception nivel REPOSITORIES (bd) e lanço a nivel servico para controller
            }
        }
    }
}
