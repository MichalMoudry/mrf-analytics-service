namespace AnalyticsService.Transport.Validation

open AnalyticsService.Transport.Contracts.Requests
open FluentValidation

/// A validator class for BatchStatRequest record.
type internal BatchStatsRequestValidator() as this =
    inherit AbstractValidator<BatchStatsRequest>()
    do
        this.RuleFor(fun i -> i.StartDate).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.WorkflowId).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.NumberOfDocs).GreaterThan(0) |> ignore
        // TODO: Add status validation