using System.Collections.Generic;

namespace WebSalesMvc.Models.ViewModels
{
    public class SellerFormViewModel //ViewModel tem a lógica de apresentacao. Representacao dos dados que serão mostrados na view
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }//deve-se utilizar os nomes corretamente pois isso ajuda o framework a reconhecer
                                                                //os dados quando for fazer a conversao de http para objeto
    }
}
