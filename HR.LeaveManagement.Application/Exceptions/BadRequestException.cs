
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string messagge) : base(messagge)
    {             
    }


    public BadRequestException(string messagge, ValidationResult validationResult) : base(messagge)
    {
        ValidationErrors = new();

        foreach(var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
    public List<string> ValidationErrors { get; set; } 
}

