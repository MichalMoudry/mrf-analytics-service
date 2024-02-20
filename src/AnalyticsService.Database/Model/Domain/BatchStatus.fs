namespace AnalyticsService.Database.Model.Domain

/// An enum class representing a final status of a document batch.
type BatchStatus =
    | Completed = 0
    | Failed = 1
