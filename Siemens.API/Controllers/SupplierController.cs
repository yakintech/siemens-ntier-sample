using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siemens.API.Models.Filters;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Entity;
using Siemens.Dto.Models.Response;
using Siemens.Dto.Models.Supplier.Request;

namespace Siemens.API.Controllers
{


    [Route("api/supplier")]
    public class SupplierController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [RoleFilter(1)]
        public IActionResult Get()
        {
            var suppliers = _unitOfWork.SupplierRepository.GetAllWithQueryable()
                .Include(x => x.Products)
                .Include(x => x.SupplierAddress)
                .Select(x => new SupplierListResponseDto()
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    AddDate = x.AddDate,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    SupplierListAddressResponseDto = new SupplierListAddressResponseDto()
                    {
                        City = x.SupplierAddress.City,
                        Region = x.SupplierAddress.Region,
                        Country = x.SupplierAddress.Country
                    },
                    Products = x.Products.Select(q => new SupplierListProductResponseDto()
                    {
                        Name = q.Name,
                        Description = q.Description
                    }).ToList()

                }).ToList();



            return Ok(suppliers);
        }


        [HttpPost]
        public IActionResult Add(SupplierCreateRequestDto model)
        {
            Supplier supplier = new Supplier();
            supplier.CompanyName = model.CompanyName;
            supplier.ContactName = model.ContactName;
            supplier.ContactTitle = model.ContactTitle;

            List<Product> products = new List<Product>();

            foreach (var item in model.Products)
            {
                var product = _unitOfWork.ProductRepository.GetById(item);
                products.Add(product);
            }

            supplier.Products = products;

            _unitOfWork.SupplierRepository.Add(supplier);
            _unitOfWork.Commit();

            SupplierAddress supplierAddress = new SupplierAddress();
            supplierAddress.Id = supplier.Id;
            supplierAddress.City = model.SupplierAddressCreateRequestDto.City;
            supplierAddress.Region = model.SupplierAddressCreateRequestDto.Region;
            supplierAddress.Country = model.SupplierAddressCreateRequestDto.Country;

            _unitOfWork.SupplierAddressRepository.Add(supplierAddress);
            _unitOfWork.Commit();



            return Ok();
        }
    }
}
