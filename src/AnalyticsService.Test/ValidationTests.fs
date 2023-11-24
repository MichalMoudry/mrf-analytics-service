module AnalyticsService.Test.ValidationTests

open System
open System.Text.Json
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
        WorkflowId = Guid.NewGuid() 
    }
    Assert.IsTrue(validator.Validate(request).IsValid)

/// A test covering a deserialization of a message from MQ.
[<TestCase("{\"batch_id\":\"121536e8-17d5-4673-9fde-e327e7830206\",\"end_date\":\"2023-11-20T11:31:43\",\"number_of_documents\":1,\"start_date\":\"2023-11-20T11:31:43\",\"status\":1,\"workflow_id\":\"b82696de-b495-4ea1-8c19-c5eda902ce74\"}")>]
let TestActualBatchStatBody(body: string) =
    let deserializedBody =
        try
            Some(JsonSerializer.Deserialize<BatchStatRequest>(body))
        with
            | :? JsonException as err -> printfn $"Exception! %s{err.Message}\n {err}"; None
    Assert.That(deserializedBody.IsSome, Is.True)