namespace AnalyticsService.Service.Model

open System

/// A record representing a general statistic about a document batch.
type GeneralBatchStats =
    { AverageNumberOfDocs: int
      SuccessRate: float
      AverageDuration: TimeSpan }
    static member Default = {
        AverageDuration = TimeSpan.Zero
        AverageNumberOfDocs = 0
        SuccessRate = 0
    }