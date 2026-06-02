using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositires;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepositry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public ProductRepositry(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDTO ProductDTO)
        {
            if (ProductDTO == null) return false;
            var product = mapper.Map<Product>(ProductDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath = await imageManagementService.AddImageAsync(ProductDTO.Photo, ProductDTO.Name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id,

            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

  

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
            {
                return false;
            }
            var FindProduct = await context.Products.Include(m=>m.Category)
                .Include(m=>m.Photos)
                .FirstOrDefaultAsync(m=>m.Id == updateProductDTO.Id);

            if (FindProduct is null)
            {
                return false;
            }
            mapper.Map(updateProductDTO, FindProduct);

    

            var  FindPhoto = await context.Photos.Where(predicate: Photo => Photo.ProductId == updateProductDTO.Id).ToListAsync();

            foreach (var  item in FindPhoto)
{
                imageManagementService.DeleteImageAsync(src: item.ImageName);
            }

            context.Photos.RemoveRange(entities: FindPhoto);

            var  ImagePath = await imageManagementService.AddImageAsync(files: updateProductDTO.Photo, src: updateProductDTO.Name);

            var  photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = updateProductDTO.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(entities: photo);

            await context.SaveChangesAsync();
            return true;

        }

        public async Task DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(m => m.ProductId == product.Id).ToListAsync();

            foreach (var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.ImageName);
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
