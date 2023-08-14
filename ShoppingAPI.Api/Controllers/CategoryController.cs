using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Abstract;
using Shopping.Business.Concrete;
using ShoppingAPI.Api.Validation.FluentValidation;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.DTO.User;
using ShoppingAPI.Entity.Poco;
using ShoppingAPI.Entity.Result;
using ShoppingAPI.Helper.CustomException;
using System.Net;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("ShoppingAPI/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("/AddCategory")]
        [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> AddCategory(CategoryDTORequest categoryDTORequest)
        {
            CategoryValidator categoryValidator = new CategoryValidator();

            if (categoryValidator.Validate(categoryDTORequest).IsValid)
            {
            
                Category category = _mapper.Map<Category>(categoryDTORequest);

                await _categoryService.AddAsync(category);
                CategoryDTOResponse categoryDTOResponse=_mapper.Map<CategoryDTOResponse>(category);
              
                return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDTOResponse));
            }
            else
            {
                List<string> validationMessages = new();
                for (int i = 0; i < categoryValidator.Validate(categoryDTORequest).Errors.Count; i++)
                {
                    validationMessages.Add(categoryValidator.Validate(categoryDTORequest).Errors[i].ErrorMessage);
                }

                //return BadRequest(Sonuc<CategoryDTOResponse>.FieldValidationError(validationMessages));

                throw new FieldValidationException(validationMessages);
            }
          
        }

        [HttpGet("/Categories")]
        [ProducesResponseType(typeof(Sonuc<List<CategoryDTOResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
           var categories =  await _categoryService.GetAllAsync(q=>q.IsActive==true && q.IsDeleted==false);

            if (categories!=null)
            {
                List<CategoryDTOResponse> categoryDtoResponseList = new List<CategoryDTOResponse>();

                foreach (var category in categories)
                {
                    categoryDtoResponseList.Add(_mapper.Map<CategoryDTOResponse>(category));
                }
                
                return Ok(Sonuc<List<CategoryDTOResponse>>.SuccessWithData(categoryDtoResponseList));
            }

            else
            {
                return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
            }
        }

        //[HttpGet("/Category/{id}")]
        //[ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]

        //public async Task<IActionResult>GetCategoryByID(int id)
        //{
        //    var category = await _categoryService.GetAsync(q => q.ID == id);

        //    if (category!=null)
        //    {
        //        CategoryDTOResponse categoryDTOResponse = new()
        //        {
        //            Name = category.Name,
        //            Guid = category.GUID,
        //        };

        //        return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDTOResponse));
        //    }
        //    else
        //    {
        //        return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
        //    }
        //}

        [HttpGet("/Category/{guid}")]
        [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryByGUID(Guid guid)
        {
            var category = await _categoryService.GetAsync(q => q.GUID == guid);

            if (category != null)
            {
                CategoryDTOResponse categoryDTOResponse = _mapper.Map<CategoryDTOResponse>(category);

                return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDTOResponse));
            }
            else
            {
                return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
            }
        }

        [HttpPost("/UpdateCategory")]

        public async Task<IActionResult>UpdateCategory(CategoryUpdateDTORequest categoryUpdateDTORequest)
        {
            Category category = await _categoryService.GetAsync(q => q.GUID == categoryUpdateDTORequest.Guid);

            category.Name = categoryUpdateDTORequest.Name;
            category.IsActive = categoryUpdateDTORequest.IsActive != null ? categoryUpdateDTORequest.IsActive : category.IsActive;

            await _categoryService.UpdateAsync(category);

            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithoutData());
        }

        [HttpPost("/RemoveCategory/{categoryGUID}")]
        [ProducesResponseType(typeof(Sonuc<bool>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult>RemoveCategory(Guid categoryGUID)
        {

            Category category = await _categoryService.GetAsync(q => q.GUID == categoryGUID);


            //category.IsActive = !category.IsActive;

            category.IsActive = false;
            category.IsDeleted = true;

            await _categoryService.UpdateAsync(category);

            return Ok(Sonuc<bool>.SuccessWithData(true));
        }

    }
}
