using FluentValidation;
using ShoppingAPI.Entity.DTO.Category;

namespace ShoppingAPI.Api.Validation.FluentValidation
{
    public class CategoryValidator:AbstractValidator<CategoryDTORequest>
    {
        public CategoryValidator()
        {
            RuleFor(q => q.Name).NotEmpty().WithMessage("Kategori Adı Boş Olamaz");
        }
    }
}
