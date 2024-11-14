using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcProductDemo.Protos;

namespace GrpcProductDemo.Services;

public class ProductService : ProductProtoService.ProductProtoServiceBase
{
    private static readonly List<Product> _products = [];

    public override Task<Product> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found"));
        }

        return Task.FromResult(product);
    }

    public override Task<ProductListResponse> ListProduct(Empty request, ServerCallContext context)
    {
        var response = new ProductListResponse();
        response.Product.AddRange(_products);

        if (response.Product.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No products found"));
        }

        return Task.FromResult(response);
    }

    public override Task<Product> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var newProduct = new Product()
        {
            Id = _products.Count + 1,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };
        
        _products.Add(newProduct);
        return Task.FromResult(newProduct);
    }

    public override Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found"));
        }
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        return Task.FromResult(product);
    }

    public override Task<Empty> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        var product = _products.FirstOrDefault(p => p.Id == request.Id);
        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found"));
        }

        _products.Remove(product);
        return Task.FromResult(new Empty());
    }
}