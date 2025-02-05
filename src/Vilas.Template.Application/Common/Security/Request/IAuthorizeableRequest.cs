using MediatR;

namespace Vilas.Template.Application.Common.Security.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{ }
