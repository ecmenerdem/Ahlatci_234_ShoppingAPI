using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
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

        public Sonuc(T _data, string _message, int _statusCode)
        {
            this.Data = _data;
            this.StatusCode = _statusCode;
            this.Mesaj = _message;
        }

        public Sonuc(string _message, int _statusCode)
        {
            this.StatusCode = _statusCode;
            this.Mesaj = _message;
        }

        public Sonuc(string _message, int _statusCode, HataBilgisi _hataBilgisi)
        {
            this.StatusCode = _statusCode;
            this.Mesaj = _message;
            this.HataBilgisi = _hataBilgisi;
        }


        public T Data { get; set; }
        public string Mesaj { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public HataBilgisi HataBilgisi { get; set; }

        public static Sonuc<T>SuccessWithData(T data, string message="İşlem Başarılı", int statusCode=(int)HttpStatusCode.OK)
        {
            return new Sonuc<T>(data, message, statusCode);
        }

        public static Sonuc<T> SuccessWithoutData(string message = "İşlem Başarılı", int statusCode = (int)HttpStatusCode.OK)
        {
            return new Sonuc<T>(message, statusCode);
        }

        public static Sonuc<T> SuccessNoDataFound(string message = "Sonuç Bulunamadı", int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new Sonuc<T>(message, statusCode,HataBilgisi.NotFound());
        }


        public static Sonuc<T> AuthenticationError(string message = "Kullanıcı Bulunamadı", int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new Sonuc<T>(message, statusCode, HataBilgisi.AuthenticationError());
        }
    }
}
