using Tech.Test.Payment.Application.Sales.Commands.CreateSale;
using Tech.Test.Payment.Application.Sales.Common;
using Tech.Test.Payment.Domain.Sales;
using TestCommon.Sale;
using TestCommon.Sales;

namespace Tech.Test.Payment.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateSaleCommand, ErrorOr<Sale>> _validationBehavior;
    private readonly Mock<IValidator<CreateSaleCommand>> _mockValidator;
    private readonly Mock<RequestHandlerDelegate<ErrorOr<Sale>>> _mockNextBehavior;

    public ValidationBehaviorTests()
    {
        _mockValidator = new Mock<IValidator<CreateSaleCommand>>();
        _mockNextBehavior = new Mock<RequestHandlerDelegate<ErrorOr<Sale>>>();

        _validationBehavior = new ValidationBehavior<CreateSaleCommand, ErrorOr<Sale>>(_mockValidator.Object);
    }

    //[Fact]
    //public async Task InvokeValidationBehavior_WhenValidatorResultIsValid_ShouldInvokeNextBehavior()
    //{
    //    // Arrange
    //    var setReminderCommand = SaleCommandFactory.CreateSaleCommand("123","321", new List<ItemSaleDto>());
    //    var reminder = SalesFactory.CreateSale();

    //    _mockValidator
    //        .Setup(v => v.ValidateAsync(setReminderCommand, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new ValidationResult());

    //    _mockNextBehavior
    //        .Setup(n => n.Invoke())
    //        .ReturnsAsync(reminder);

    //    // Act
    //    var result = await _validationBehavior.Handle(setReminderCommand, _mockNextBehavior.Object, default);

    //    // Assert
    //    result.IsError.Should().BeFalse();
    //    result.Value.Should().BeEquivalentTo(reminder);
    //    _mockNextBehavior.Verify(n => n.Invoke(), Times.Once);
    //}

    //[Fact]
    //public async Task InvokeValidationBehavior_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    //{
    //    // Arrange
    //    var setReminderCommand = ReminderCommandFactory.CreateSetReminderCommand();
    //    var validationFailures = new List<ValidationFailure> { new ValidationFailure("foo", "bad foo") };

    //    _mockValidator
    //        .Setup(v => v.ValidateAsync(setReminderCommand, It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new ValidationResult(validationFailures));

    //    // Act
    //    var result = await _validationBehavior.Handle(setReminderCommand, _mockNextBehavior.Object, default);

    //    // Assert
    //    result.IsError.Should().BeTrue();
    //    result.FirstError.Code.Should().Be("foo");
    //    result.FirstError.Description.Should().Be("bad foo");
    //    _mockNextBehavior.Verify(n => n.Invoke(), Times.Never);
    //}

    //[Fact]
    //public async Task InvokeValidationBehavior_WhenNoValidator_ShouldInvokeNextBehavior()
    //{
    //    // Arrange
    //    var setReminderCommand = ReminderCommandFactory.CreateSetReminderCommand();
    //    var validationBehavior = new ValidationBehavior<SetReminderCommand, ErrorOr<Reminder>>();

    //    var reminder = ReminderFactory.CreateReminder();
    //    _mockNextBehavior
    //        .Setup(n => n.Invoke())
    //        .ReturnsAsync(reminder);

    //    // Act
    //    var result = await validationBehavior.Handle(setReminderCommand, _mockNextBehavior.Object, default);

    //    // Assert
    //    result.IsError.Should().BeFalse();
    //    result.Value.Should().Be(reminder);
    //    _mockNextBehavior.Verify(n => n.Invoke(), Times.Once);
    //}
}

