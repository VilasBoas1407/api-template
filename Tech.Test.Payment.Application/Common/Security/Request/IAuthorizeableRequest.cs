using MediatR;

namespace Tech.Test.Payment.Application.Common.Security.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{ }
