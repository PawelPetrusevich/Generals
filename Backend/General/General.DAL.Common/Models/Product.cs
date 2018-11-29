using System.Threading.Tasks;
using General.DAL.Common.Attributes;

namespace General.DAL.Common.Models
{
    [TableName("Products")]
    public class Product : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}