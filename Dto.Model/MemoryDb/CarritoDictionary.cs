using Dto.Model.Dtos;

namespace Dto.Model.MemoryDb
{
    public class CarritoDictionary
    {
        private readonly Dictionary<int, carritoconsultaDto> _carrito = new Dictionary<int, carritoconsultaDto>();

        public IReadOnlyDictionary<int, carritoconsultaDto> Carrito => _carrito;

        public void AddOrUpdate(int productId, carritoconsultaDto product)
        {
            _carrito[productId] = product;
        }

        public void LoadProducts(IEnumerable<carritoconsultaDto> products)
        {
            foreach (var product in products)
            {
                _carrito[product.CarritoId] = product;
            }
        }

        public void Delete(int productId)
        {
            _carrito.Remove(productId);
        }
    }
}
