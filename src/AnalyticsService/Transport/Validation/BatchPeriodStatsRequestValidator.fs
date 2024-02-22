namespace AnalyticsService.Transport.Validation

open AnalyticsService.Transport.Contracts.Requests
open FluentValidation

/// Validator class for BatchPeriodStatRequest record.
type internal BatchPeriodStatsRequestValidator() as this =
    inherit AbstractValidator<BatchPeriodStatsRequest>()
    do
        this.RuleFor(fun i -> i.WorkflowId).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.StartDate).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.EndDate)
            .NotEmpty()
            .GreaterThan(fun i -> i.StartDate)
            |> ignore