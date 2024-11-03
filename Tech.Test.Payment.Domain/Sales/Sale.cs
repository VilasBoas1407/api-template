using Tech.Test.Payment.Domain.Common;
using Tech.Test.Payment.Domain.Sells;

namespace Tech.Test.Payment.Domain.Sales;

public class Sale : Entity
{
    public Sale(Guid? id, string customerName, string customerPhone, 
        Guid sellerId, string sellerName,
        string sellerCpf, string sellerEmail)
        : base(id ?? Guid.NewGuid())
    {
        CustomerPhone = customerPhone;
        SellerCpf = sellerCpf;
        SellerEmail = sellerEmail;
        Status = SaleStatus.WaitingPayment;
        SaleDate = DateTime.UtcNow;
        SellerId = sellerId;
        SellerName = sellerName;
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
    public ICollection<ItemSale> Items { get; protected set; }

    public void AddItem(ItemSale item)
    {
        Items.Add(item);
    }

    public void RemoveItem(ItemSale item) { }
}
