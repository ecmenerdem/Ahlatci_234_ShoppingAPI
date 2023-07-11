using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Entity.Result
{
    public class Sonuc<T>
    {
        public Sonuc(T _data, string _mesaj, int _statusCode, HataBilgisi _hataBilgisi)
        {
            this.Data = _data;
            this.Mesaj=_mesaj;
            this.StatusCode = _statusCode;
            this.HataBilgisi=_hataBilgisi;
        }
        public T Data { get; set; }
        public string Mesaj { get; set; }
        public int StatusCode { get; set; }
        public HataBilgisi HataBilgisi { get; set; }

    }
}
