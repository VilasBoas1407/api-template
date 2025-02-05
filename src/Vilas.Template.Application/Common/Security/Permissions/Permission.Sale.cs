namespace Vilas.Template.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Sale
    {
        public const string Create = "create:sale";
        public const string Get = "get:sale";
        public const string AddItem = "add:item-sale";
        public const string RemoveItem = "remove:item-sale";
        public const string UpdateStatus = "update:sale-status";
    }
}
