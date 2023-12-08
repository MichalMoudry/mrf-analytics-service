namespace AnalyticsService.Transport.Validation

open AnalyticsService.Transport.Contracts.Requests
open FluentValidation

/// A validator class for BatchStatRequest record.
[<Sealed>]
type BatchStatRequestValidator() as this =
    inherit AbstractValidator<BatchStatRequest>()
    do
        this.RuleFor(fun i -> i.StartDate).NotEmpty() |> ignore
        this
            .RuleFor(fun i -> i.EndDate)
            .NotEmpty()
            .GreaterThan(fun i -> i.StartDate) |> ignore
        this.RuleFor(fun i -> i.Status).NotEmpty() |> ignore
        this.RuleFor(fun i -> i.NumberOfDocuments).GreaterThan(0) |> ignore