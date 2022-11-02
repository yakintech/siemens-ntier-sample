using Siemens.BLL.Service.Repositories.Entity;
using Siemens.BLL.Service.Repositories.Interfaces;
using Siemens.DAL.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ISupplierAddressRepository SupplierAddressRepository { get; private set; }

        private SiemensContext _siemensContext;
        public UnitOfWork(SiemensContext siemensContext)
        {
            _siemensContext = siemensContext;

            ProductRepository = new ProductRepository(_siemensContext);
            SupplierRepository = new SupplierRepository(_siemensContext);
            SupplierAddressRepository = new SupplierAddressRepository(_siemensContext);
        }

        public void Commit()
        {
            _siemensContext.SaveChanges();
        }
    }
}
