using System.Threading.Tasks;

namespace General.DAL.Common.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}