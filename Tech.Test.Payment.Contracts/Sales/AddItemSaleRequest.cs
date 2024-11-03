namespace Tech.Test.Payment.Contracts.Sales
{
    public record AddItemSaleRequest(string ItemName, int ItemQuantity, decimal ItemPrice);
}
