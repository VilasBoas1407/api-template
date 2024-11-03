using Tech.Test.Payment.Domain.Sells;

namespace Tech.Test.Payment.Contracts.Sales;

public record GetSaleResponse(Guid Id, 
    string CustomerName, 
    string CustomerPhone,
    Guid SellerId,
    string SellerName,
    string SellerCpf,
    string SellerEmail,
    string SellerPhone, 
    SaleStatus Status, 
    DateTime SaleDate, 
    IReadOnlyList<GetSaleItemResponse> Items);

public record GetSaleItemResponse(Guid Id, string Name, decimal Price, int Quantity);