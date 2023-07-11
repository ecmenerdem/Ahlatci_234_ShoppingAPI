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

            return Ok(new Sonuc<CategoryDTOResponse>(categoryDTOResponse,"İşlem Başarılı", (int)HttpStatusCode.OK, null));
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

                return Ok(new Sonuc<List<CategoryDTOResponse>>(categoryDtoResponseList, "İşlem Başarılı", (int)HttpStatusCode.OK, null));
            }

            else
            {
                return NotFound(new Sonuc<List<CategoryDTOResponse>>(null, "Sonuç Bulunamadı", 200, new HataBilgisi()
                {
                    Hata = null,
                    HataAciklama = "Sonuç Bulunamadı"
                }));
            }
        }

        [HttpGet("/Category/{id}")]
        [ProducesResponseType(typeof(Sonuc<CategoryDTOResponse>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult>GetCategoryByID(int id)
        {
            var category = await _categoryService.GetAsync(q => q.ID == id);

            if (category!=null)
            {
                CategoryDTOResponse categoryDTOResponse = new()
                {
                    Name = category.Name,
                    Guid = category.GUID,
                };
                return Ok(new Sonuc<CategoryDTOResponse>(categoryDTOResponse, "İşlem Başarılı", (int)HttpStatusCode.OK, null));
            }
            else
            {
                return NotFound(new Sonuc<CategoryDTOResponse>(null, "Sonuç Bulunamadı", (int)HttpStatusCode.NotFound, new HataBilgisi()
                {
                    Hata=null,
                    HataAciklama="Sonuç Bulunamadı"
                }));
            }
        }

        //[HttpGet("/Category/{guid}")]
        //public async Task<IActionResult>GetCategoryByGUID(Guid guid)
        //{

        //}
    }
}
