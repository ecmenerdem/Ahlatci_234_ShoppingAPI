using FluentValidation;
using ShoppingAPI.Entity.DTO.Login;

namespace ShoppingAPI.Api.Validation.FluentValidation
{
    public class LoginValidator:AbstractValidator<LoginRequestDTO>
    {
        public LoginValidator()
        {
            RuleFor(q => q.KullaniciAdi).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
            RuleFor(q => q.Sifre).NotEmpty().WithMessage("Şifre Boş Olamaz");
        }
    }
}
