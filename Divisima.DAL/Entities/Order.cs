using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class Order
    {
        public int ID { get; set; }

        [Column(TypeName ="Varchar(20)"), StringLength(20), Display(Name ="Sipariş Numarası")]
        public string OrderNumber { get; set; }

        [Display(Name = "Ödeme Seçeneği")]
        public EPaymentOption PaymentOption{ get; set; }

        [Display(Name = "Sipariş Durumu")]
        public EOrderStatus OrderStatus { get; set; }

        [Display(Name = "Sipariş Tarihi")]
        public DateTime RecDate { get; set; }


        /*---------------------*/
        [Column(TypeName = "Varchar(100)"), StringLength(100), Display(Name = "Teslimat Adresi")]
        public string Address { get; set; }

        [Column(TypeName = "Varchar(50)"), StringLength(50), Display(Name = "Teslimat Şehri")]
        public string City { get; set; }

        [Column(TypeName = "Varchar(50)"), StringLength(50), Display(Name = "Teslimat İlçesi")]
        public string Distirct { get; set; }

        [Column(TypeName = "Varchar(50)"), StringLength(50), Display(Name = "Teslimat Ülkesi")]
        public string Country { get; set; }

        [Column(TypeName = "Varchar(10)"), StringLength(10), Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Column(TypeName = "Varchar(20)"), StringLength(20), Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Column(TypeName = "Varchar(80)"), StringLength(80), Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Column(TypeName = "Varchar(100)"), StringLength(100), Display(Name = "Adı Soyadı")]
        public string NameSurname { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Kargo Ücreti")]
        public decimal ShipFee { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string CardNumber { get; set; }
        [NotMapped]
        public string CardMonth { get; set; }
        [NotMapped]
        public string CardYear { get; set; }
        [NotMapped]
        public string CardCV2 { get; set; }
    }
}
