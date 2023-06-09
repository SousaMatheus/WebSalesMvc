using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSalesMvc.Data;
using WebSalesMvc.Models;
using WebSalesMvc.Services.Exceptions;

namespace WebSalesMvc.Services
{
    public class SellerService
    {
        private readonly WebSalesMvcContext _context;
        public SellerService(WebSalesMvcContext context)
        {
            _context = context;
        }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();       
        }
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);       
        }
        public async Task RemoveAsync (int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Remove(obj);    
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't remove seller because he/she has sales");
            }
        }

        internal void Details(int id)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(Seller obj)  
        {
            var teste = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!teste)             
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
                throw new DbConccurrencyException(ex.Message);            
            }
        }
    }
}
