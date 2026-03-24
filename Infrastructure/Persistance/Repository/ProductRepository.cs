using Application.Interface;
using Domain.Aggregate.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
         await _context.Products.AddAsync(product);
        }

        public async Task<List<Product?>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
           return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product?>> GetByIdAsync(List<Guid> id)
        {
           return await _context.Products.Where(p => id.Contains(p.Id)).ToListAsync();
        }
    }
}
