
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string messagge) : base(messagge)
    {             
    }


    public BadRequestException(string messagge, ValidationResult validationResult) : base(messagge)
    {
        ValidationErrors = validationResult.ToDictionary();
    }
    public IDictionary<string, string[]> ValidationErrors { get; set; } 
}

