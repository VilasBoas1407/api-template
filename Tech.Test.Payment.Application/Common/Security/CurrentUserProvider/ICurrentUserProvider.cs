namespace Tech.Test.Payment.Application.Common.Security.CurrentUserProvider;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}
