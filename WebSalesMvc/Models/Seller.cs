using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSalesMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage =  "{0} required")]//{0} e o nome do atributo
        [StringLength(70, MinimumLength =  3, ErrorMessage = "{0} must be btween {2} and {1}")]//{1} primeiro parametro
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat (DataFormatString = "{0:F2}")]//0 significa o valor do atributo e a formatacao dele
        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 100000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }// framework entende quer quero guardar o ID do department, colocando como int que e struct (nao pode ser nulo)
                                             // o Seller precisa de um departamento, integridade referencial
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
