using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class ProductPicture
    {
        public int ID { get; set; }

        [Column(TypeName ="Varchar(100)"), StringLength(100), Display(Name ="Ürün Resim Adı"), Required(ErrorMessage = "Ürün Resim Adı Boş Geçilemez")]
        public string Name { get; set; }


        [Column(TypeName = "Varchar(150)"), StringLength(150), Display(Name = "Resim Dosyası")]
        public string Picture { get; set; }


        [Display(Name ="Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
