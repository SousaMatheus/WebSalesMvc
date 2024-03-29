﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSalesMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
            //Quando um objeto possui uma coleção de objetos de outro tipo, não faz sentido informar como parâmetro
            //uma instância vazia dessa coleção como parâmetro na hora de criar o objeto.
            //A instanciação dessa coleção deve ser responsabilidade do próprio objeto, e não do programa principal.
            //O objeto já deve ser criado normalmente com a coleção instanciada,
           //daí o que o objeto deve permitir é que você adicione ou remova elementos da coleção.
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales (SalesRecord sr)
        {
            SalesRecords.Add(sr);   
        }
        public void RemoveSales (SalesRecord sr)
        {
            SalesRecords.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return SalesRecords.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
