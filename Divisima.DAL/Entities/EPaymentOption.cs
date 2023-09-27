using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public enum EPaymentOption
    { 
        [Display(Name ="Kredi Kartı ile Ödeme")]
        KrediKartı = 1,
        [Display(Name = "Havale ile Ödeme")]
        Havale = 2,
        [Display(Name = "Kapıda Nakit veya Kredi Kartı ile Ödeme")]
        KapıdaOdeme = 3
    }
}
