module AnalyticsService.Test.ModelsTest

open System
open NUnit.Framework
open AnalyticsService.Database.Domain

/// Test covering a basic initialization of a single instance of the document batch statistic.
[<Test>]
let TestBatchStatInit () =
    let startDate = DateTime(2023, 9, 26)
    let endDate = DateTime(2023, 9, 27)
    let stat = NewBatchStat startDate endDate 5 BatchStatus.Success
    Assert.IsTrue(stat.IsSome)
    Assert.That(stat.Value.RunTime, Is.EqualTo(TimeSpan.FromDays(1)))

[<Test>]
let TestIncorrectBatchStatInit () =
    let startDate = DateTime(2023, 9, 27)
    let endDate = DateTime(2023, 9, 26)
    let stat = NewBatchStat startDate endDate 5 BatchStatus.Success
    Assert.IsTrue(stat.IsNone)