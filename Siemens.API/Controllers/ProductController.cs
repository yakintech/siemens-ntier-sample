using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Siemens.API.Models.Filters;
using Siemens.BLL.Service;
using Siemens.BLL.Service.Repositories;
using Siemens.DAL.ORM.Context;
using Siemens.DAL.ORM.Entity;
using Siemens.Dto.Models;

namespace Siemens.API.Controllers
{
    [Route("api/product")]
    public class ProductController : BaseController
    {

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IMemoryCache _memortyCache;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memortyCache = memoryCache;
        }

        [HttpGet]
        [ResponseHeaderFilter("x","10")]
        public IActionResult Get()
        {

            string key = "productList";

            if (_memortyCache.TryGetValue(key, out object list))
            {
                return Ok(list);
            }

            var products = _unitOfWork.ProductRepository.GetAllWithQueryable().Include(q => q.Category).Select(x => new ProductListResponseDto()
            {
                Id = x.Id,
                Name = x.Name,
                UnitsInStock = x.UnitsInStock,
                Description = x.Description,
                Category = new CategoryDto()
                {
                    Name = x.Category.Name,
                    Guid = x.Category.Id
                }

            }).ToList();

            _memortyCache.Set(key, products, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(2),
                Priority = CacheItemPriority.Normal,
            });




            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);

            if (product != null)
            {
                ProductDetailResponseDto model = new ProductDetailResponseDto();
                model.Id = product.Id;
                model.Name = product.Name;
                model.UnitsInStock = product.UnitsInStock;
                model.Description = product.Description;
                model.AddDate = product.AddDate;

                return Ok(model);
            }
            else
            {
                throw new DataNotFoundException(id.ToString());
            }
        }


        [HttpPost]
        public IActionResult Add(CreateProductRequestDto createProductDto)
        {
           // Product product2 = new Product();
            //product.Name = createProductDto.Name;
            //product.Description = createProductDto.Description;
            //product.UnitsInStock = createProductDto.UnitsInStock;


             var product = _mapper.Map<Product>(createProductDto);

             var newProduct =  _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();

            CreateProductResponseDto model = new CreateProductResponseDto();
            model.Id = newProduct.Id;
            model.Name = newProduct.Name;
            model.Description = newProduct.Description;
            model.AddDate = newProduct.AddDate;
            model.UnitsInStock = newProduct.UnitsInStock;


            return StatusCode(201, model);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            _unitOfWork.ProductRepository.Remove(id);
            return Ok();
        }

        
    }
}
