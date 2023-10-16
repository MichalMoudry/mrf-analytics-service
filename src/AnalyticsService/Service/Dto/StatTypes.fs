namespace AnalyticsService.Service.Dto

open System

type GeneralAppStats = {
    AverageNumberOfDocs: int
    SuccessRate: float
    AverageDuration: TimeSpan
}
