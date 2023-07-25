using FluentValidation;
using FluentValidation.Results;
using ShoppingAPI.Helper.CustomException;

namespace ShoppingAPI.Api.Validation.FluentValidation
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Inline veya Attribute üzerinden validasyon kontrolü için kullanılır.
        /// </summary>
        /// <param name="type">Validator tipi</param>
        /// <param name="items">Valide edilecek nesneler (Array)</param>
        public static void Validate(Type type, object[] items)
        {
            //verilen tip ile validator oluşturma durumu kontrol ediliyor.
            if (!typeof(IValidator).IsAssignableFrom(type))
                throw new Exception("Hata: Validator tipi geçersiz!");

            var validator = (IValidator)Activator.CreateInstance(type);

            //gelen tüm argumanlar için validasyon yapılacak.
            foreach (var item in items)
            {
                //arguman tipi valide edilebiliyor mu bakılıyor.
                if (validator.CanValidateInstancesOfType(item.GetType()))
                {
                    var result = validator.Validate(new ValidationContext<object>(item));

                    //eğer valid durumda değilse hatalar dönülür.
                    //valid ise void metod işletilir ve çıkılır.
                    List<string> ValidationMessages = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        ValidationMessages.Add(error.ErrorMessage);
                    }

                    if (!result.IsValid)
                        throw new FieldValidationException(ValidationMessages);
                }
            }
        }

        /// <summary>
        /// Inline olarak validasyon işlemi yapar.
        /// </summary>
        /// <param name="type">Validator tipi</param>
        /// <param name="item">Valide edilecek nesne</param>
        /// <returns>ValidationResult tipinde Validasyon sonucu dönüşünü sağlar</returns>
        public static ValidationResult Validate(Type type, Object item)
        {
            //verilen tip ile validator oluşturma durumu kontrol ediliyor.
            if (!typeof(IValidator).IsAssignableFrom(type))
                throw new Exception("Hata: Validator tipi geçersiz!");

            var validator = (IValidator)Activator.CreateInstance(type);

            //valid veya valid olmama durumunun tamamı result olarak dönülür.
            return validator.Validate(new ValidationContext<object>(item));
        }
    }
}

