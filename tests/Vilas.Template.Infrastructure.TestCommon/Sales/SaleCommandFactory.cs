using Tech.Test.Payment.Application.Sales.Commands.CreateSale;
using Tech.Test.Payment.Application.Sales.Common;

namespace TestCommon.Sale;

public static class SaleCommandFactory
{
    public static CreateSaleCommand CreateSaleCommand(string customerName, 
        string customerPhone, IList<ItemSaleDto> items)
    {
        return new CreateSaleCommand(customerName, customerPhone, items);
    }
}
