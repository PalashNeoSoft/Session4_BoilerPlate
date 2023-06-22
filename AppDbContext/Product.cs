using System.ComponentModel.DataAnnotations;

namespace Session4_BoilerPlate.AddDbContext
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }



    }
}
