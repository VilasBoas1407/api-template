using ErrorOr;
using Tech.Test.Payment.Domain.Common;
using Tech.Test.Payment.Domain.Sells;

namespace Tech.Test.Payment.Domain.Sales;

public class Sale : Entity
{
    public Sale()
    {  }

    public Sale(Guid? id, string customerName, string customerPhone,
        Guid sellerId, string sellerName, string sellerCpf, string sellerEmail, string sellerPhone)
        : base(id ?? Guid.NewGuid())
    {
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        Status = SaleStatus.WaitingPayment;
        SaleDate = DateTime.UtcNow;
        SellerId = sellerId;
        SellerName = sellerName;
        SellerCpf = sellerCpf;
        SellerEmail = sellerEmail;
        SellerPhone = sellerPhone;
        Items = [];
    }

    public string CustomerName { get; protected set; }
    public string CustomerPhone { get; protected set; }
    public Guid SellerId { get; protected set; }
    public string SellerName { get; protected set; }
    public string SellerCpf { get; protected set; }
    public string SellerEmail { get; protected set; }
    public string SellerPhone { get; protected set; }
    public SaleStatus Status { get; protected set; }
    public DateTime SaleDate { get; protected set; }
    public virtual ICollection<ItemSale> Items { get; protected set; }

    public void AddItem(ItemSale item)
    {
        Items.Add(item);
    }

    public ErrorOr<Success> RemoveItem(Guid idItemSale) { 
    
        var item = Items.FirstOrDefault(x => x.Id == idItemSale);

        if (item == null)
            return Error.NotFound(description: $"Não foi encontrada um item com o Id :{idItemSale} na venda.");

        Items.Remove(item);

        return Result.Success;
    }
}
