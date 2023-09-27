using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class Admin
    {
        public int ID { get; set; }

        [Column(TypeName = "Varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"), Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; set; }

        [Column(TypeName = "Varchar(32)"), StringLength(32), Display(Name = "Parola"), Required(ErrorMessage = "Parola Boş Geçilemez")]
        public string Password { get; set; }

        [Column(TypeName = "Varchar(50)"), StringLength(50), Display(Name = "Admin Adı"), Required(ErrorMessage ="Ad Soyad Kısmı Boş Bırakılamaz")]
        public string NameSurname { get; set; }

        [Display(Name ="Son Giriş Tarihi")]
        public DateTime LastLongDate { get; set; }

        [Column(TypeName ="varchar(20)"), StringLength(20), Display(Name = "Admin IP"), Required(ErrorMessage ="Son Giriş No")]
        public string LastLoginIP { get; set; }
    }
}
