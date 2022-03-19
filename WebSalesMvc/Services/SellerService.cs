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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();//retorna do banco de dados todos os vendedores
        }
        public async Task InsertAync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);//necessario utilizar a expressao nao nativa do Linq
                                                                                                      //Include para realizar eager loading
        }
        public async Task Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Remove(obj);//remove o obj do dbset
            await _context.SaveChangesAsync();//efetiva na bd
        }

        internal void Details(int id)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Seller obj)//atualizar um vendedor
        {
            var teste = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!teste)//verifica se tem no banco de dados um vendedor com id igual do objeto
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new DbConccurrencyException(ex.Message);//intercepto a exception nivel REPOSITORIES (bd) e lanço a nivel servico para controller
            }
        }
    }
}
