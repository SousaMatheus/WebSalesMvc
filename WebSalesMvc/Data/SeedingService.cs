using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSalesMvc.Models;
using WebSalesMvc.Models.Enums;

namespace WebSalesMvc.Data
{
    public class SeedingService
    {
        private WebSalesMvcContext _context;

        public SeedingService(WebSalesMvcContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if(_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return;
            }

            Department computers = new Department(new int(), "Computer");             
            Department electronics = new Department(new int(), "Electronics");
            Department hardware = new Department(new int(), "Hardware");
            Department accessories = new Department(new int(), "Accessories");

            Seller matheus = new Seller(new int(), "Matheus Sousa", "matheus.sousa10@gmail.com", new DateTime(1990, 01, 01), 7500.89, computers);
            Seller matias = new Seller(new int(), "Matias Defrederico", "matias.france@gmail.com", new DateTime(1985, 02, 02), 2500.15, hardware);
            Seller mathieu = new Seller(new int(), "Mathiue", "mathieu.france@outlook.com", new DateTime(1995, 12, 12), 2500.15, accessories);
            Seller anna = new Seller(new int(), "Anna Green", "anna.green111@outlook.com", new DateTime(1998, 01, 20), 3500.75, electronics);
            Seller bob = new Seller(new int(), "Bob Brown", "bob.bob@gmail.com", new DateTime(190, 09, 05), 2500.15, computers);
            Seller alex = new Seller(new int(), "Alex Silva", "alex_silva123@gmail.com", new DateTime(1985, 02, 02), 2500.15, hardware);
            Seller ramon = new Seller(new int(), "Ramon Menezes", "ramon_ramon123@outlook.com", new DateTime(2000, 12, 12), 2500.15, accessories);
            Seller mariana = new Seller(new int(), "Mariana Mendes", "anna@outlook.com", new DateTime(1998, 01, 20), 3800.75, electronics);

            SalesRecord sale1 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 22,10, 233), 4899.90, SaleStatus.Billed, matheus);
            SalesRecord sale2 = new SalesRecord(new int(), new DateTime(2022, 02, 07,08, 10, 05, 200), 999.90, SaleStatus.Canceled, mariana);
            SalesRecord sale3 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 18, 30, 983), 91.90, SaleStatus.Pending, anna);
            SalesRecord sale4 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 09, 04, 05, 500), 299.00, SaleStatus.Billed, alex);
            SalesRecord sale5 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 09, 22, 10, 233), 89.90, SaleStatus.Billed, mathieu);
            SalesRecord sale6 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 09, 25, 05, 200), 19.90, SaleStatus.Canceled, matias);
            SalesRecord sale7 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 09, 22, 10, 233), 2340.53, SaleStatus.Pending, matheus);
            SalesRecord sale8 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 09, 23, 44, 250), 3000.90, SaleStatus.Billed, matheus);
            SalesRecord sale9 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 22, 10, 433), 4899.90, SaleStatus.Billed, ramon);
            SalesRecord sale10 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 10, 05, 700), 199.90, SaleStatus.Billed, mariana);
            SalesRecord sale11 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 22, 10, 133), 91.90, SaleStatus.Billed, anna);
            SalesRecord sale12 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 10, 05, 002), 299.00, SaleStatus.Billed, anna);
            SalesRecord sale13 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 22, 10, 230), 400.00, SaleStatus.Billed, mathieu);
            SalesRecord sale14 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 10, 05, 299), 19.90, SaleStatus.Canceled, matias);
            SalesRecord sale15 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 22, 10, 999), 1370.00, SaleStatus.Canceled, matheus);
            SalesRecord sale16 = new SalesRecord(new int(), new DateTime(2022, 02, 07, 08, 10, 05, 100), 300.90, SaleStatus.Billed, matheus);

            _context.Department.AddRange(computers, electronics, hardware, accessories);
            _context.Seller.AddRange(matheus, matias, mathieu, anna, bob, alex, ramon, mariana);
            _context.SalesRecord.AddRange(sale1, sale2, sale3, sale4, sale5, sale6, sale7, sale8, sale9,
                sale10, sale11, sale12, sale13, sale14, sale15, sale16);

            _context.SaveChanges();
        }
    }
}
