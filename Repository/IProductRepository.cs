using Session4_BoilerPlate.AddDbContext;

namespace Session4_BoilerPlate.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product AddProduct(Product product);

        Product GetProductById(int id);

        bool UpdateProduct(Product product);

        bool DeleteProduct(Product product);


    }
}
