namespace AnalyticsService.Web.Api.Transport.Validation

open AnalyticsService.Web.Api.Transport.Contracts.Requests
open FluentValidation

/// Validator class for BatchPeriodStatRequest record.
type BatchPeriodStatRequestValidator() as this =
    inherit AbstractValidator<BatchPeriodStatRequest>()
    do
        this.RuleFor(fun i -> i.WorkflowId).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.StartDate).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.EndDate)
            .NotEmpty()
            .GreaterThan(fun i -> i.StartDate)
            |> ignore