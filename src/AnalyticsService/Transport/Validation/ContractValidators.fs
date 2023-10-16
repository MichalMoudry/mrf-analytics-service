module AnalyticsService.Transport.Validation

open FluentValidation
open AnalyticsService.Transport.Contracts

/// A validator class for BatchStatRequest record.
type BatchStatRequestValidator () as this =
    inherit AbstractValidator<BatchStatRequest>()
    do
        this
            .RuleFor(fun request -> request.StartDate)
            .NotEmpty()
        |> ignore

        this
            .RuleFor(fun request -> request.EndDate)
            .NotEmpty()
            .GreaterThan(fun request -> request.StartDate)
        |> ignore

        this
            .RuleFor(fun request -> request.Status)
            .NotNull()
            .IsInEnum()
        |> ignore

        this
            .RuleFor(fun request -> request.NumberOfDocuments)
            .NotEmpty()
            .GreaterThan(0)
        |> ignore