using Tech.Test.Payment.Domain.Common;

namespace Tech.Test.Payment.Domain.Sales
{
    public class ItemSale : Entity
    {
        public ItemSale()
        { }

        public ItemSale(Guid id, Guid saleId, string name,
            decimal price, int quantity) : base(id)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Guid SaleId { get; protected set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public int Quantity { get; protected set; }

        public virtual Sale Sale { get; protected set; }

    }
}
