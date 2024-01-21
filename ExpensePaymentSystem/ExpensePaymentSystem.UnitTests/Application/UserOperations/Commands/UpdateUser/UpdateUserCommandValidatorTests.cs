using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;
using ExpensePaymentSystem.Business.Commands.UserCommands.UpdateUser;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.UpdateUser;

public class UpdateUserCommandValidatorTests
{
    private readonly UserRequestValidator _validator;

    public UpdateUserCommandValidatorTests()
    {
        _validator = new UserRequestValidator();
    }

    [Fact]
    public void Validate_ShouldReturnTrueForValidModel()
    {
        // Arrange
        var validModel = new UserRequest
        {
            UserName = "testuser",
            Password = "testpassword",
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890"
        };

        // Act
        var validationResult = _validator.Validate(validModel);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

}