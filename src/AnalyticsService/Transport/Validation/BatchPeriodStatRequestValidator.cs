using AnalyticsService.Transport.Contracts.Requests;
using FluentValidation;

namespace AnalyticsService.Transport.Validation;

/// <summary>
/// Validator class for <see cref="BatchPeriodStatRequest"/> record.
/// </summary>
internal sealed class BatchPeriodStatRequestValidator : AbstractValidator<BatchPeriodStatRequest>
{
    public BatchPeriodStatRequestValidator()
    {
        RuleFor(i => i.WorkflowId).NotEmpty();
        RuleFor(i => i.StartDate).NotEmpty();
        RuleFor(i => i.EndDate).NotEmpty().GreaterThan(i => i.StartDate);
    }
}