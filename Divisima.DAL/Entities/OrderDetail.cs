using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }

        [Display(Name = "Ürün ID")]
        public int ProductID { get; set; }

        [Column(TypeName ="Varchar(100)"), StringLength(100), Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Column(TypeName = "Varchar(150)"), StringLength(150), Display(Name = "Ürün Resmi")]
        public string ProductPicture { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Ürün Fiyat Bilgisi")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Miktar")]
        public int Quantity { get; set; }

    }
}
