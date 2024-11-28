
using Grpc.Core;
using Grpc.Net.Client;
using GrpcProductDemo.Protos;

var channel = GrpcChannel.ForAddress("https://localhost:7107");
var client = new ProductProtoService.ProductProtoServiceClient(channel);

var headers = new Metadata
{
    {
        "Authorization",
        "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2YmViNGViZi1hYzFkLTQyZjEtYjYzYS0xOWYxZmI2MzdkOWYiLCJleHAiOjE3MzI3OTU0ODAsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMDciLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MTA3In0.5fjTLnafZfS6JrtmDwglXR5TbBjcdNuFBrAQU60LGUE"
    }
};
// create
var create = await client.CreateProductAsync(new CreateProductRequest
{
    Name = "Product 1",
    Description = "Description 1",
    Price = 56.78
}, headers);
Console.WriteLine(create);
Console.WriteLine("=====================");

var create2 = await client.CreateProductAsync(new CreateProductRequest
{
    Name = "Product 1",
    Description = "Description 1",
    Price = 56.78
}, headers);
Console.WriteLine(create2);
Console.WriteLine("=====================");

var products = await client.ListProductAsync(new Google.Protobuf.WellKnownTypes.Empty(), headers);

foreach (var product in products.Product)
{
    Console.WriteLine(product);
}

var getProduct = await client.GetProductAsync(new GetProductRequest
{
    Id = 1
});
Console.WriteLine(getProduct);
Console.ReadLine();
