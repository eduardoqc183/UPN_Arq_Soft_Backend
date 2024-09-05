using Dto.Model.Dtos;
using Dto.Model.MemoryDb;
using MediatR;

namespace Service.Query.Producto
{
    public class ObtenerProductosQuery : IRequest<List<productoDto>>
    {
        public string NombreProducto { get; }
        public string Marca { get; }
        public decimal? PrecioMin { get; }
        public decimal? PrecioMax { get; }

        public ObtenerProductosQuery(string nombreProducto, string marca, decimal? precioMin, decimal? precioMax)
        {
            NombreProducto = (nombreProducto ?? "").ToLower().Trim();
            Marca = (marca ?? "").ToLower().Trim();
            PrecioMin = precioMin;
            PrecioMax = precioMax;
        }
    }

    public class ObtenerProductosEventHandler : IRequestHandler<ObtenerProductosQuery, List<productoDto>>
    {
        private readonly ProductDictionary productDictionary;

        public ObtenerProductosEventHandler(ProductDictionary productDictionary)
        {
            this.productDictionary = productDictionary;
        }

        public Task<List<productoDto>> Handle(ObtenerProductosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.Run(() =>
                {
                    var products = productDictionary.Products.Values.AsQueryable();

                    if (!string.IsNullOrEmpty(request.NombreProducto))
                        products = products.Where(x => x.nombre.ToLower().Contains(request.NombreProducto));

                    if (!string.IsNullOrEmpty(request.Marca))
                        products = products.Where(x => (x.marca ?? "").ToLower().Contains(request.Marca));

                    if (request.PrecioMin.HasValue)
                        products = products.Where(x => x.preciosinigv >= request.PrecioMin);

                    if (request.PrecioMax.HasValue)
                        products = products.Where(x => x.preciosinigv <= request.PrecioMax);

                    var res = products.ToList();
                    return res;
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
