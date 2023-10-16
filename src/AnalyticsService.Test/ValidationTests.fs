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
        AppId = Guid.NewGuid() 
    }
    Assert.IsTrue(validator.Validate(request).IsValid)