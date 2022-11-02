using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siemens.BLL.Service;
using Siemens.BLL.Service.Repositories;
using Siemens.DAL.ORM.Context;
using Siemens.DAL.ORM.Entity;
using Siemens.Dto.Models;

namespace Siemens.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {



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
                return NotFound();
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
