using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebSalesMvc.Models;

namespace WebSalesMvc.Data
{
    public class WebSalesMvcContext : DbContext // essa clase administra os objetos entidades durante o tempo de execução. Realiza o mapeamento entre um banco de
                                                // dados relacionale os objetos. Ele define as propriedades DbSet. Trabalhamos com uma sessão que representa uma
                                                // conexão com o bd em memoria, para persistir as informacoes no banco deve-se usar o SaveChanges
    {
        public WebSalesMvcContext (DbContextOptions<WebSalesMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } //representam(mapeiam) as entidades do meu modelo e o nome das tabelas. É auxiliar do DbCOntext
        public DbSet<SalesRecord> SalesRecord { get; set; }
        public DbSet<Seller> Seller { get; set; }

    }
}
