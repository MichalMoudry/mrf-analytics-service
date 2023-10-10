module AnalyticsService.Test.ValidationTests

open System
open NUnit.Framework
open AnalyticsService.Transport.Validation
open AnalyticsService.Transport.Contracts

/// A test covering a basic validation of a batch stat request.
[<Test>]
let TestBatchStatRequestValidation () =
    let validator = BatchStatRequestValidator()
    let request = {
        StartDate = DateTime.Now
        EndDate = DateTime.Now.AddDays(1)
        NumberOfDocuments = 5
        Status = AnalyticsService.Database.Domain.BatchStatus.Success
    }
    Assert.IsTrue(validator.Validate(request).IsValid)

/// A test covering similar scenario as TestBatchStatRequestValidation,
/// but with cloud event wrapper.
[<Test>]
let TestBatchStatRequestValidationAsCloudEvent () =
    let validator = BatchStatRequestValidator()
    let event = {
        Id = Guid.NewGuid().ToString()
        Data = {
            StartDate = DateTime.Now
            EndDate = DateTime.Now.AddDays(1)
            NumberOfDocuments = 5
            Status = AnalyticsService.Database.Domain.BatchStatus.Success
        }
        Source = "test_source" 
    }
    Assert.IsTrue(validator.Validate(event.Data).IsValid)