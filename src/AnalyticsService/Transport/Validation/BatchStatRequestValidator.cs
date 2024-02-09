using AnalyticsService.Transport.Contracts.Requests;
using FluentValidation;

namespace AnalyticsService.Transport.Validation;

/// <summary>
/// A validator class for <see cref="BatchStatRequest"/> record.
/// </summary>
internal sealed class BatchStatRequestValidator : AbstractValidator<BatchStatRequest>
{
    public BatchStatRequestValidator()
    {
        RuleFor(i => i.StartDate).NotEmpty();
        RuleFor(i => i.EndDate).NotEmpty().GreaterThan(i => i.StartDate);
        RuleFor(i => i.Status).IsInEnum();
        RuleFor(i => i.NumberOfDocuments).GreaterThan(0);
    }
}