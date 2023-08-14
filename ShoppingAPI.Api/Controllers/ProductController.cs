using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Abstract;
using ShoppingAPI.Entity.DTO.Category;
using ShoppingAPI.Entity.DTO.Product;
using ShoppingAPI.Entity.Poco;
using ShoppingAPI.Entity.Result;
using System.Net;

namespace ShoppingAPI.Api.Controllers
{
    [ApiController]
    [Route("ShoppingAPI/[action]")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService, ICategoryService categoryService)
        {
            _mapper = mapper;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet("/Products")]
        [ProducesResponseType(typeof(Sonuc<List<ProductDTOResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllAsync(q => q.IsActive == true && q.IsDeleted == false, "Category");

            if (products != null)
            {
                List<ProductDTOResponse> productDtoResponseList = new List<ProductDTOResponse>();

                foreach (var product in products)
                {
                    productDtoResponseList.Add(_mapper.Map<ProductDTOResponse>(product));

                }

                return Ok(Sonuc<List<ProductDTOResponse>>.SuccessWithData(productDtoResponseList));
            }

            else
            {
                return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
            }
        }


        [HttpGet("/Product/{productGUID}")]
        [ProducesResponseType(typeof(Sonuc<List<ProductDTOResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct(Guid productGUID)
        {
            var product = await _productService.GetAsync(q => q.GUID==productGUID, "Category");

            if (product != null)
            {
                ProductDTOResponse productDtoResponse = _mapper.Map<ProductDTOResponse>(product);
                return Ok(Sonuc<ProductDTOResponse>.SuccessWithData(productDtoResponse));
            }

            else
            {
                return NotFound(Sonuc<List<CategoryDTOResponse>>.SuccessNoDataFound());
            }
        }

        [HttpPost("/AddProduct")]
        [ProducesResponseType(typeof(Sonuc<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddProduct(ProductDTORequest productDTORequest)
        {
            var category = await _categoryService.GetAsync(q => q.GUID == productDTORequest.CategoryGuid);

            productDTORequest.CategoryID = category.ID;

            Product product = _mapper.Map<Product>(productDTORequest);

            product.Category = category;

            await _productService.AddAsync(product);

            return Ok(Sonuc<bool>.SuccessWithData(true));
        } 
        
        
        [HttpPost("/UpdateProduct")]
        [ProducesResponseType(typeof(Sonuc<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(ProductDTORequest productDTORequest)
        {

            Product product = await _productService.GetAsync(q => q.GUID == productDTORequest.Guid);

            string featuredImage = product.FeaturedImage;

            var category = await _categoryService.GetAsync(q => q.GUID == productDTORequest.CategoryGuid);

            //_mapper.Map<Product>(productDTORequest);

            product.UnitPrice = productDTORequest.UnitPrice;
            product.Name=productDTORequest.Name;
            product.Description=productDTORequest.Description;
            product.FeaturedImage = featuredImage;

            if (productDTORequest.FeaturedImage is null)
            {
                product.FeaturedImage = featuredImage;
            }

            product.Category = category;
            await _productService.UpdateAsync(product);

            return Ok(Sonuc<bool>.SuccessWithData(true));
        }


        [HttpPost("/RemoveProduct/{productGUID}")]
        [ProducesResponseType(typeof(Sonuc<bool>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult>RemoveProduct(Guid productGUID)
        {
            Product product = await _productService.GetAsync(q => q.GUID == productGUID);

            product.IsActive = false;
            product.IsDeleted = true;
           await _productService.UpdateAsync(product);
            return Ok(Sonuc<bool>.SuccessWithData(true));
        }
    }
}

