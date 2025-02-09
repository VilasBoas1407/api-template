using Vilas.Template.Domain.Sells;

namespace Vilas.Template.Domain.UnitTests.Sales;

public class SaleTests
{
    [Fact]
    public void CreateSale_ValidParameters_ShouldCreateSale()
    {
        // Arrange
        var customerName = "John Doe";
        var customerPhone = "1234567890";
        var sellerId = Guid.NewGuid();
        var sellerName = "Jane Doe";
        var sellerCpf = "12345678901";
        var sellerEmail = "jane@example.com";
        var sellerPhone = "0987654321";

        // Act
        var sale = new Sale(null, customerName, customerPhone, sellerId, sellerName, sellerCpf, sellerEmail, sellerPhone);

        // Assert
        Assert.NotNull(sale);
        Assert.Equal(customerName, sale.CustomerName);
        Assert.Equal(customerPhone, sale.CustomerPhone);
        Assert.Equal(SaleStatus.WaitingPayment, sale.Status);
        Assert.Equal(DateTime.UtcNow.Date, sale.SaleDate.Date);
        Assert.Equal(sellerId, sale.SellerId);
        Assert.Equal(sellerName, sale.SellerName);
        Assert.Equal(sellerCpf, sale.SellerCpf);
        Assert.Equal(sellerEmail, sale.SellerEmail);
        Assert.Equal(sellerPhone, sale.SellerPhone);
    }

    [Fact]
    public void AddItem_ShouldAddItemToSale()
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");
        var item = new ItemSale(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 10.0m, 1);

        // Act
        sale.AddItem(item);

        // Assert
        Assert.Contains(item, sale.Items);
    }

    [Fact]
    public void RemoveItem_ExistingItem_ShouldRemoveItem()
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");
        var item = new ItemSale(Guid.NewGuid(), Guid.NewGuid(), "Item 1", 10.0m, 1);
        sale.AddItem(item);

        // Act
        var result = sale.RemoveItem(item.Id);

        // Assert
        Assert.False(result.IsError);
        Assert.DoesNotContain(item, sale.Items);
    }

    [Fact]
    public void RemoveItem_NonExistingItem_ShouldReturnNotFoundError()
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");
        var itemId = Guid.NewGuid(); // Id que não existe

        // Act
        var result = sale.RemoveItem(itemId);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal($"Não foi encontrada um item com o Id :{itemId} na venda.", result.Errors.First().Description);
    }

    [Theory]
    [InlineData(SaleStatus.PaymentApproved, SaleStatus.PaymentApproved, "O novo status deve ser diferente do status atual.")]
    [InlineData(SaleStatus.WaitingPayment, SaleStatus.Send, "Novo status inválido - A venda só pode ser alterada para")]
    public void UpdateStatus_InvalidTransition_ShouldReturnConflictError(SaleStatus initialStatus, SaleStatus newStatus, string expected)
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");
        sale.UpdateStatus(initialStatus);

        // Act
        var result = sale.UpdateStatus(newStatus);

        // Assert
        Assert.True(result.IsError);

        Assert.Contains(expected, result.Errors.First().Description);

    }

    [Theory]
    [InlineData(SaleStatus.WaitingPayment, SaleStatus.PaymentApproved)]
    [InlineData(SaleStatus.WaitingPayment, SaleStatus.Canceled)]
    [InlineData(SaleStatus.PaymentApproved, SaleStatus.Send)]
    [InlineData(SaleStatus.PaymentApproved, SaleStatus.Canceled)]
    public void UpdateStatus_ValidTransition_ShouldUpdateStatus(SaleStatus initialStatus, SaleStatus newStatus)
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");

        sale.UpdateStatus(initialStatus);

        // Act
        var result = sale.UpdateStatus(newStatus);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(newStatus, sale.Status);
    }

    [Fact]
    public void UpdateStatus_DeliveredStatus_ShouldReturnConflictError()
    {
        // Arrange
        var sale = new Sale(null, "Customer", "1234567890", Guid.NewGuid(), "Seller", "12345678901", "seller@example.com", "0987654321");
        sale.UpdateStatus(SaleStatus.PaymentApproved);
        sale.UpdateStatus(SaleStatus.Send);
        sale.UpdateStatus(SaleStatus.Delivered);

        // Act
        var result = sale.UpdateStatus(SaleStatus.Canceled);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("A venda não pode ter o status alterado, pois já foi entregue", result.Errors.First().Description);
    }
}


