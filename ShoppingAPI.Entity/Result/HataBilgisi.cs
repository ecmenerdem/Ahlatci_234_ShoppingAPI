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

        public static HataBilgisi NotFound(string hataAciklama = "Sonuç Bulunamdı", object? hata = null)
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };

        }
        public static HataBilgisi AuthenticationError(string hataAciklama = "Kullanıcı Bulunamadı", object? hata = null)
        {
            return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
        }

    }
}
