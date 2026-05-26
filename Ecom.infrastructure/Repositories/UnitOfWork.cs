using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly AppDbContext _context;

        public ICategoryRepositry categoryRepositry { get; }
        public IPhotoRepositry photoRepositry { get; }

        public IProductRepositry productRepositry { get; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            categoryRepositry = new CategoryRepositry(_context);
            photoRepositry = new PhotoRepositry(_context);
            productRepositry = new ProductRepositry(_context);

        }
    }
}
