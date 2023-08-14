using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Entity.Result
{
    public class HataBilgisi
    {
        public string HataAciklama { get; set; }
        public object Hata { get; set; }

        public static HataBilgisi NotFound(string hataAciklama = "Sonuç Bulunamadı", object? hata = null)
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };

        }

        public static HataBilgisi Error(string hataAciklama = "Genel Bir Hata Oluştu", object? hata = null)
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };

        }
        public static HataBilgisi AuthenticationError(string hataAciklama = "Kullanıcı Bulunamadı", object? hata = null)
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
        }

        public static HataBilgisi FieldValidationError(object? hata = null, string hataAciklama = "Zorunlu Alanlarda Eksiklikler Var")
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
        }
        
        public static HataBilgisi TokenError(object? hata = null, string hataAciklama = "Token Hatası")
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
        }
        public static HataBilgisi TokenNotFoundError(object? hata = null, string hataAciklama = "Token Bilgisi Gelmedi")
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
        }

    }
}
