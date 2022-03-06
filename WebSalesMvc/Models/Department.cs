using System.Collections.Generic;
using System;
using System.Linq;

namespace WebSalesMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()//needed empty constructor for Framework
        {
        }

        public Department(int iD, string name)
        {
            Id = iD;
            Name = name;
        }
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(sellers => sellers.TotalSales(initial, final));
        }
    }
}
