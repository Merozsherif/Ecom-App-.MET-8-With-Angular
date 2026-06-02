using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
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
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ICategoryRepositry categoryRepositry { get; }
        public IPhotoRepositry photoRepositry { get; }

        public IProductRepositry productRepositry { get; }


        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
            categoryRepositry = new CategoryRepositry(_context);
            photoRepositry = new PhotoRepositry(_context);
            productRepositry = new ProductRepositry(_context,_mapper,_imageManagementService);
          
        }
    }
}
