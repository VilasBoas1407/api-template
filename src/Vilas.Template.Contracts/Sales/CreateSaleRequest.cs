namespace Vilas.Template.Contracts.Sales;


public record CreateSaleRequest(string CustomerName, string CustomerPhone, IList<CreateSaleItemRequest> Items);
public record CreateSaleItemRequest(string ItemName, int ItemQuantity,decimal ItemPrice);