namespace AnalyticsService.Service.Model

open System

/// A record representing a general statistic about a document batch.
type GeneralBatchStats =
  { AvgNumberOfDocs: int
    SuccessRate: float
    AvgDuration: TimeSpan }
    static member Default = { AvgNumberOfDocs = 0; SuccessRate = 0; AvgDuration = TimeSpan.Zero }