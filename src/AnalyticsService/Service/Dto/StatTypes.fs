namespace AnalyticsService.Service.Dto

open System

type GeneralAppStats =
    { AverageNumberOfDocs: int
      SuccessRate: float
      AverageDuration: TimeSpan }
    static member Default =
        { AverageDuration = TimeSpan.Zero
          AverageNumberOfDocs = 0
          SuccessRate = 0 }
