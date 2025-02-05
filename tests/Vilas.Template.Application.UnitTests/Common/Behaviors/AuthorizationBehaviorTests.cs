
using ErrorOr;
using MediatR;
using Vilas.Template.Application.Common.Behaviors;
using Vilas.Template.Application.Common.Interfaces.Services;
using Vilas.Template.Application.Common.Security.Request;

namespace Vilas.Template.Application.UnitTests.Common.Behaviors;

public class AuthorizationBehaviorTests
{

    private readonly Mock<IAuthorizationService> _mockAuthorizationService;
    private readonly Mock<RequestHandlerDelegate<ErrorOr<Response>>> _mockNextBehavior;

    public AuthorizationBehaviorTests()
    {
        _mockAuthorizationService = new Mock<IAuthorizationService>();

        _mockNextBehavior = new Mock<RequestHandlerDelegate<ErrorOr<Response>>>();
        _mockNextBehavior
            .Setup(x => x.Invoke())
            .ReturnsAsync(Response.Instance);
    }

    [Fact]
    public async Task InvokeAuthorizationBehavior_WhenNoAuthorizationAttribute_ShouldInvokeNextBehavior()
    {
        // Arrange
        var request = new RequestWithNoAuthorizationAttribute();
        var authorizationBehavior = new AuthorizationBehavior<RequestWithNoAuthorizationAttribute, ErrorOr<Response>>(_mockAuthorizationService.Object);

        // Act
        var result = await authorizationBehavior.Handle(request, _mockNextBehavior.Object, default);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(Response.Instance, result.Value);
        _mockNextBehavior.Verify(x => x.Invoke(), Times.Once);
    }

    [Fact]
    public async Task InvokeAuthorizationBehavior_WhenSingleAuthorizationAttributeAndUserIsAuthorized_ShouldInvokeNextBehavior()
    {
        // Arrange
        var request = new RequestWithSingleAuthorizationAttribute();

        _mockAuthorizationService
            .Setup(x => x.AuthorizeCurrentUser(
                request,
                It.IsAny<List<string>>(),
                It.Is<List<string>>(p => p.SequenceEqual(new[] { "Permission" })),
                It.IsAny<List<string>>()))
            .Returns(Result.Success);

        var authorizationBehavior = new AuthorizationBehavior<RequestWithSingleAuthorizationAttribute, ErrorOr<Response>>(_mockAuthorizationService.Object);

        // Act
        var result = await authorizationBehavior.Handle(request, _mockNextBehavior.Object, default);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(Response.Instance, result.Value);
        _mockNextBehavior.Verify(x => x.Invoke(), Times.Once);
    }

    [Fact]
    public async Task InvokeAuthorizationBehavior_WhenSingleAuthorizationAttributeAndUserIsNotAuthorized_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new RequestWithSingleAuthorizationAttribute();
        var error = Error.Unauthorized(code: "bad.user", description: "bad user");

        _mockAuthorizationService
            .Setup(x => x.AuthorizeCurrentUser(
                request,
                It.IsAny<List<string>>(),
                It.Is<List<string>>(p => p.SequenceEqual(new[] { "Permission" })),
                It.IsAny<List<string>>()))
            .Returns(error);

        var authorizationBehavior = new AuthorizationBehavior<RequestWithSingleAuthorizationAttribute, ErrorOr<Response>>(_mockAuthorizationService.Object);

        // Act
        var result = await authorizationBehavior.Handle(request, _mockNextBehavior.Object, default);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(error, result.FirstError);
        _mockNextBehavior.Verify(x => x.Invoke(), Times.Never);
    }

    [Fact]
    public async Task InvokeAuthorizationBehavior_WhenTonsOfAuthorizationAttributesAndUserIsAuthorized_ShouldInvokeNextBehavior()
    {
        // Arrange
        var request = new RequestWithTonsOfAuthorizationAttribute();

        _mockAuthorizationService
            .Setup(x => x.AuthorizeCurrentUser(
                request,
                It.Is<List<string>>(r => r.SequenceEqual(new[] { "Role1", "Role2", "Role3" })),
                It.Is<List<string>>(p => p.SequenceEqual(new[] { "Permission1", "Permission2", "Permission3" })),
                It.Is<List<string>>(pl => pl.SequenceEqual(new[] { "Policy1", "Policy2", "Policy3" }))))
            .Returns(Result.Success);

        var authorizationBehavior = new AuthorizationBehavior<RequestWithTonsOfAuthorizationAttribute, ErrorOr<Response>>(_mockAuthorizationService.Object);

        // Act
        var result = await authorizationBehavior.Handle(request, _mockNextBehavior.Object, default);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal(Response.Instance, result.Value);

        _mockNextBehavior.Verify(x => x.Invoke(), Times.Once);
    }

    [Fact]
    public async Task InvokeAuthorizationBehavior_WhenTonsOfAuthorizationAttributesAndUserIsNotAuthorized_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new RequestWithTonsOfAuthorizationAttribute();
        var error = Error.Unauthorized(code: "bad.user", description: "bad user");

        _mockAuthorizationService
            .Setup(x => x.AuthorizeCurrentUser(
                request,
                It.Is<List<string>>(r => r.SequenceEqual(new[] { "Role1", "Role2", "Role3" })),
                It.Is<List<string>>(p => p.SequenceEqual(new[] { "Permission1", "Permission2", "Permission3" })),
                It.Is<List<string>>(pl => pl.SequenceEqual(new[] { "Policy1", "Policy2", "Policy3" }))))
            .Returns(error);

        var authorizationBehavior = new AuthorizationBehavior<RequestWithTonsOfAuthorizationAttribute, ErrorOr<Response>>(_mockAuthorizationService.Object);

        // Act
        var result = await authorizationBehavior.Handle(request, _mockNextBehavior.Object, default);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(error, result.FirstError);

        _mockNextBehavior.Verify(x => x.Invoke(), Times.Never);
    }
}

public record Response
{
    public static readonly Response Instance = new();
}

public record RequestWithNoAuthorizationAttribute() : IAuthorizeableRequest<ErrorOr<Response>>;

[Authorize(Permissions = "Permission")]
public record RequestWithSingleAuthorizationAttribute() : IAuthorizeableRequest<ErrorOr<Response>>;

[Authorize(Permissions = "Permission1,Permission2")]
[Authorize(Roles = "Role1,Role2")]
[Authorize(Policies = "Policy1,Policy2")]
[Authorize(Permissions = "Permission3", Roles = "Role3", Policies = "Policy3")]
public record RequestWithTonsOfAuthorizationAttribute() : IAuthorizeableRequest<ErrorOr<Response>>;

