using Siemens.BLL.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.BLL.Service
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ISupplierAddressRepository SupplierAddressRepository { get;  }
        IWebUserRepository WebUserRepository { get; }   

        void Commit();
    }
}
