using Tech.Test.Payment.Application.Common.Dtos;

namespace Tech.Test.Payment.Application.Common.Interfaces.Services;

public interface IUserContextService
{
    UserContextDto GetCurrentUser();
}
