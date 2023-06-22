using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Session4_BoilerPlate.AddDbContext;


namespace Session4_BoilerPlate.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public ProductRepository(ProductDbContext productDbContext , IDataProtectionProvider dataProtectionProvider)
        {
            _productDbContext = productDbContext;
            _dataProtectionProvider = dataProtectionProvider;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _productDbContext.Products.ToList();
            var proteted = _dataProtectionProvider.CreateProtector("ProductEncode");
            foreach (var product in products)
            {
                product.ProductName = proteted.Unprotect(product.ProductName);
                
            }
            return products;

            /*return _productDbContext.Products.ToList();*/
        }


        public Product AddProduct(Product product)
        {
            var proteted = _dataProtectionProvider.CreateProtector("ProductEncode");

            product.ProductName = proteted.Protect(product.ProductName);
            _productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();
            return product;

            /*_productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();
            return product;*/
        }

        public Product GetProductById(int id)
        {
            return _productDbContext.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public bool UpdateProduct(Product product)
        {
            _productDbContext.Products.Update(product);
            int affectedRows = _productDbContext.SaveChanges();
            return affectedRows > 0;
        }

        public bool DeleteProduct(Product product)
        {
            _productDbContext.Products.Remove(product);
            int affectedRows = _productDbContext.SaveChanges();
            return affectedRows > 0;
        }





    }
}
