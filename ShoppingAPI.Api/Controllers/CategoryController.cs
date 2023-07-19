using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Abstract;
using Shopping.Business.Concrete;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.DTO.User;
using ShoppingAPI.Entity.Poco;
using ShoppingAPI.Entity.Result;
using System.Net;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("ShoppingAPI/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("/AddCategory")]
        [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> AddCategory(CategoryDTORequest categoryDTORequest)
        {
            Category category = new()
            {
                Name = categoryDTORequest.Name
            };

           await _categoryService.AddAsync(category);

            CategoryDTOResponse categoryDTOResponse = new()
            {
                Guid = category.GUID,
                Name = category.Name
            };

            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDTOResponse));
        }

        [HttpGet("/Categories")]
        [ProducesResponseType(typeof(Sonuc<List<CategoryDTOResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategories()
        {
           var categories =  await _categoryService.GetAllAsync();

            if (categories!=null)
            {
                List<CategoryDTOResponse> categoryDtoResponseList = new List<CategoryDTOResponse>();

                foreach (var category in categories)
                {
                    categoryDtoResponseList.Add(new CategoryDTOResponse()
                    {
                        Guid = category.GUID,
                        Name = category.Name
                    });
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
                CategoryDTOResponse categoryDTOResponse = new()
                {
                    Name = category.Name,
                    Guid = category.GUID,
                };

                return Ok(Sonuc<CategoryDTOResponse>.SuccessWithData(categoryDTOResponse));
            }
            else
            {
                return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
            }
        }

        [HttpPost("/UpdateCategory")]

        public async Task<IActionResult>UpdateCategory(CategoryDTORequest categoryDTORequest)
        {
            Category category = await _categoryService.GetAsync(q => q.GUID == categoryDTORequest.Guid);

            category.Name = categoryDTORequest.Name;
            category.IsActive = categoryDTORequest.IsActive != null ? categoryDTORequest.IsActive : category.IsActive;

            await _categoryService.UpdateAsync(category);

            return Ok(Sonuc<CategoryDTOResponse>.SuccessWithoutData());
        }

    }
}
