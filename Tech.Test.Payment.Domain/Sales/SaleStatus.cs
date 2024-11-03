using System.ComponentModel;

namespace Tech.Test.Payment.Domain.Sells;

public enum SaleStatus
{
    [Description("Aguardando Pagamento")]
    WaitingPayment,
    [Description("Pagamento Aprovado")]
    PaymentApproved,
    [Description("Enviado para transporte")]
    Send,
    [Description("Entregue")]
    Delivered,
    [Description("Cancelada")]
    Canceled
}
