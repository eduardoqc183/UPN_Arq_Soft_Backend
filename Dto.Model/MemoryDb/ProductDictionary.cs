using Dto.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Model.MemoryDb
{
    public class ProductDictionary
    {
        private readonly Dictionary<int, productoDto> _products = new Dictionary<int, productoDto>();

        public IReadOnlyDictionary<int, productoDto> Products => _products;

        public void AddOrUpdate(int productId, productoDto product)
        {
            _products[productId] = product;
        }

        public void LoadProducts(IEnumerable<productoDto> products)
        {
            foreach (var product in products)
            {
                _products[product.productoid] = product;
            }
        }
    }

}
